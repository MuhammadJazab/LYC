import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientConstants, DaysOfWeek, ServiceStatus } from 'src/app/Shared/Common/Common';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { DialogsComponent } from '../../Shared/Layout/dialogs/dialogs.component';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit {
  new: boolean = false;
  appValForm: FormGroup;
  formSubmitted: boolean = false;
  isUpdatingValue: boolean = false;
  haveFacilities: boolean = false;
  serviceList: any = [];
  FacilityList: any = [];
  branchesList: any = [];
  serviceTimeSlots: any = [];
  checkedDayList: any = [];
  checkedFacilityList: any = [];
  facilities: string = '';
  selectedServiceStatus = 0;
  serviceStatus = ServiceStatus;
  dayList = DaysOfWeek;

  selectedBranch = 0;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.appValForm = this.formBuilder.group({
      'serviceName': ['', [Validators.required]],
      'branchId': ['', [Validators.required]],
      'serviceStatus': ['', [Validators.required]],
      'serviceCost': ['', [Validators.required]],
      'startingDateTime': ['', [Validators.required]],
      'endingDateTime': ['', [Validators.required]],
      'maxOccupants': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }
  get f() { return this.appValForm.controls }

  refreshData() {
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
    this.getServiceList();
    this.getBranchesList();
  }

  getServiceList() {
    this.common.get(this.common.apiRoutes.Configuration.Service.GetServices).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.serviceList = response.resultData;
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

  getBranchFacilitiesList(event: any) {
    this.common.get(this.common.apiRoutes.Configuration.Branch.GetBranchFacilitiesByBranchId + '?branchId=' + parseInt(event.target.value)).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.FacilityList = response.resultData;
        if (this.FacilityList.length > 0) {
          this.haveFacilities = true;
          this.appValForm.get("branchId")?.patchValue(+event.target.value);
        }
        else {
          this.haveFacilities = false;
        }
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

  save() {
    this.formSubmitted = true;
    if (this.appValForm.invalid) {
      return;
    }
    else if (this.checkedDayList == undefined && this.checkedDayList.length <= 0) {
      this.snackbar.ShowSnackbar("Select days for which service is available.", "OK");
      return;
    }
    else if (this.haveFacilities && this.checkedFacilityList == undefined && this.checkedFacilityList.length <= 0) {
      this.snackbar.ShowSnackbar("Select facilities for which service is available.", "OK");
      return;
    }
    else {
      let data = this.appValForm.value;
      let postUrl = "";

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Service.UpdateService;
      }
      else {
        postUrl = this.common.apiRoutes.Configuration.Service.AddNewService;
      }

      data.Rooms = this.checkedFacilityList;
      data.Days = this.checkedDayList;

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("configuration/services");
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

  updateService(item: any) {
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

  deleteBranch(serviceId: bigint) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Service.DeleteService + '?serviceId=' + serviceId).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
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
    });
  }

  onDayListSelectionChange(option: any, event: any) {
    if (event.target.checked) {
      this.checkedDayList.push(option.id);
    } else {
      for (var i = 0; i < this.branchesList.length; i++) {
        if (this.checkedDayList[i] == option.id) {
          this.checkedDayList.splice(i, 1);
        }
      }
    }
  }

  onFacilityListSelectionChange(option: any, event: any) {
    if (event.target.checked) {
      this.checkedFacilityList.push(option);
    } else {
      for (var i = 0; i < this.branchesList.length; i++) {
        if (this.checkedFacilityList[i] == option.roomId) {
          this.checkedFacilityList.splice(i, 1);
        }
      }
    }
  }

  saveSchedule() {
    this.formSubmitted = true;
  }

  cancelSchedule() {
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
  }

  cancel() {
    this.new = false;
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
  }
}
