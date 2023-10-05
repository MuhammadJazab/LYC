import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ClientConstants } from 'src/app/Shared/Common/Common';
import { DialogsComponent } from 'src/app/Shared/Layout/dialogs/dialogs.component';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-branches',
  templateUrl: './branches.component.html',
  styleUrls: ['./branches.component.css']
})

export class BranchesComponent implements OnInit {
  public new: boolean = false;
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public branchesList: any = [];
  public showDeletedMessage: boolean = false;
  public isUpdatingValue: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.appValForm = this.formBuilder.group({
      'registrationNumber': ['', [Validators.required]],
      'branchName': ['', [Validators.required]],
      'personInCharge': ['', [Validators.required]],
      'address': ['', [Validators.required]],
      'telephoneNumber': ['', [Validators.required]],
      'cSTelNumber': ['', [Validators.required]],
      'email': ['', [Validators.required]],
      'webSite': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData() {
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
    this.getBranchesList();
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

  save() {
    this.formSubmitted = true;
    if (this.appValForm.invalid) {
      return;
    }
    else {
      let data = this.appValForm.value;
      let postUrl = "";

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Branch.UpdateBranch;
      }
      else {
        postUrl = this.common.apiRoutes.Configuration.Branch.AddNewBranch;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("configuration/branches");
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

  updateBranch(item: any) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });
    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.appValForm.patchValue(item);
        this.new = true;
        this.isUpdatingValue = true;
      }
    });
  }


  deleteBranch(registrationNumber: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Branch.DeleteBranch + '?registrationNumber=' + registrationNumber).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
            this.showDeletedMessage = true;
            this.refreshData();
            setTimeout(() => this.showDeletedMessage = false, 3000);
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

  cancel() {
    this.new = false;
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
  }
}
