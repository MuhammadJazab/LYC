import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClientConstants } from 'src/app/Shared/Common/Common';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/Services/httpclient.service';
import { DialogsComponent } from 'src/app/Shared/Layout/dialogs/dialogs.component';
import { MatDialog } from '@angular/material/dialog';



@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  public departmentForm: FormGroup;
  public departmentFormSubmitted: boolean = false;
  public departmentList: any = [];
  public isUpdatingValue: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.departmentForm = this.formBuilder.group({
      'departmentId': [''],
      'departmentName': ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData() {
    this.departmentFormSubmitted = false;
    this.isUpdatingValue = false;
    this.getDepartmentList();
    this.departmentForm.reset();
  }
  get f() { return this.departmentForm.controls }

  getDepartmentList() {
    this.common.get(this.common.apiRoutes.Configuration.Department.GetDepartments).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.departmentList = response.resultData;
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

  saveDepartment() {
    this.departmentFormSubmitted = true;
    if (this.departmentForm.invalid) {
      return;
    }
    else {
      let postUrl = "";
      let departmentData = this.departmentForm.value;

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Department.UpdateDepartment;
      }
      else {
        postUrl = this.common.apiRoutes.Configuration.Department.AddNewDepartment;
        departmentData.departmentId = null;
      }

      this.common.post(postUrl, departmentData).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.common.NavigateToRoute("configuration/department");
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

  deleteDepartment(departmentId: bigint) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Department.DeleteDepartment + '?departmentId=' + departmentId).subscribe(response => {
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

  updateDepartment(departmentId: bigint) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });
    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.isUpdatingValue = true;
        var departmentDetails = this.departmentList.filter((department: { departmentID: bigint; }) => department.departmentID == departmentId)[0];
        let form = this.departmentForm.setValue({
          'departmentId': departmentDetails.departmentID,
          'departmentName': departmentDetails.departmentName
        });
      }
    });
  }

}
