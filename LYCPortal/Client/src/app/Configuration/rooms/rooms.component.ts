import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientConstants, RoomStatus, RoomType } from 'src/app/Shared/Common/Common';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { DialogsComponent } from '../../Shared/Layout/dialogs/dialogs.component';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  public new: boolean = false;
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public roomList: any = [];
  public branchesList: any = [];

  selectedRoomStatus = 0;
  roomStatus = RoomStatus;

  selectedRoomType = '';
  roomType = RoomType;
  isAccomodationSelected: boolean = true;

  isUpdatingValue: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.appValForm = this.formBuilder.group({
      'roomId': [''],
      'branchId': ['', [Validators.required]],
      'roomType': ['', [Validators.required]],
      'roomNumber': ['', [Validators.required]],
      'roomName': ['', [Validators.required]],
      'roomStatus': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData() {
    this.getRooms();
    this.getBranchesList();
    this.appValForm.reset();
  }

  get f() { return this.appValForm.controls }

  getBranchesList() {
    this.common.get(this.common.apiRoutes.Configuration.Branch.GetAllBranches).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.branchesList = response.resultData;
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK")
      }
    }, exception => {
      let responseMessage = ClientConstants.Messages.GenericError;
      if (exception.headers.has(ClientConstants.SessionConstants.ExpiredToken) && exception.headers.get(ClientConstants.SessionConstants.ExpiredToken) == 'true') {
        localStorage.clear();
        this.router.navigate([''])
        responseMessage = exception.headers.get(ClientConstants.SessionConstants.ExpiredTokenMessage)
      }
      this.snackbar.ShowSnackbar(exception.message + '. ' + responseMessage, "OK");
    });
  }

  getRooms() {
    this.common.get(this.common.apiRoutes.Configuration.Room.GetRooms).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.roomList = response.resultData;
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK")
      }
    }, exception => {
      this.snackbar.ShowSnackbar(exception.message, "OK");
    });
  }

  save() {
    this.formSubmitted = true;

    if (this.appValForm.invalid) {
      return;
    }
    else {
      let postUrl = "";
      let data = this.appValForm.value;

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Room.UpdateRoom;
      }
      else {
        data.roomId = 0;
        postUrl = this.common.apiRoutes.Configuration.Room.AddNewRoom;
      }

      if (this.isAccomodationSelected) {
        data.accomodationChoice = 1;
      }
      else {
        data.accomodationChoice = 2;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("configuration/rooms");
          this.refreshData();
        }
        else {
          this.snackbar.ShowSnackbar(response.message, "OK");
        }
      }, exception => {
        let responseMessage = ClientConstants.Messages.GenericError;
        if (exception.headers.has(ClientConstants.SessionConstants.ExpiredToken) && exception.headers.get(ClientConstants.SessionConstants.ExpiredToken) == 'true') {
          localStorage.clear();
          this.router.navigate([''])
          responseMessage = exception.headers.get(ClientConstants.SessionConstants.ExpiredTokenMessage)
        }
        this.snackbar.ShowSnackbar(exception.message + '. ' + responseMessage, "OK");
      });
    }
  }

  deleteRoom(roomId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Room.DeleteRoom + '?roomId=' + roomId).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
            this.refreshData();
          }
          else {
            this.snackbar.ShowSnackbar(response.message, "OK");
          }
        }, exception => {
          this.snackbar.ShowSnackbar(exception.message, "OK");
        });
      }
    });
  }

  updateRoom(item: any) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.new = true;
        this.appValForm.patchValue(item);
        this.isUpdatingValue = true;
      }
    });
  }

  onChangeRadioButton(selectedType: boolean) {
    this.isAccomodationSelected = selectedType;
  }

  cancel() {
    this.new = false;
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
  }
}
