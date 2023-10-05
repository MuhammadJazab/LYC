import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientConstants } from 'src/app/Shared/Common/Common';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { DialogsComponent } from '../../Shared/Layout/dialogs/dialogs.component';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent implements OnInit {
  new: boolean = false;
  public staffForm: FormGroup;
  public roleForm: FormGroup;
  staffFormSubmitted: boolean = false;
  roleFormSubmitted: boolean = false;
  rolesList: any = [];
  staffList: any = [];
  departmentList: any = [];
  checkedBranchList: any = [];
  branchesList: any = [];
  type = "new";
  isUpdatingValue: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.staffForm = this.formBuilder.group({
      'userId': [''],
      'userName': ['', [Validators.required]],
      'department': ['', [Validators.required]],
      'email': ['', [Validators.required]],
      'password': ['', [Validators.required]],
      'contact': ['', [Validators.required]],
      'userRole': ['', [Validators.required]],
    });

    this.roleForm = this.formBuilder.group({
      'roleName': ['', [Validators.required]],
      'id': ['']
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }



  refreshData() {
    this.staffFormSubmitted = false;
    this.roleFormSubmitted = false;
    this.getRoles();
    this.getStaff();
    this.getBranchesList();
    this.getDepartmentList();
    this.roleForm.reset();
    this.staffForm.reset();
  }
  get r() { return this.roleForm.controls }
  get s() { return this.staffForm.controls }

  getRoles() {

    this.common.get(this.common.apiRoutes.Identity.GetRoles).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.rolesList = response.resultData;
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

  getStaff() {
    this.common.get(this.common.apiRoutes.Identity.GetAllUsers).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.staffList = response.resultData;
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

  saveStaff() {
    this.staffFormSubmitted = true;

    if (this.staffForm.invalid) {
      return;
    }
    else if (this.checkedBranchList == undefined && this.checkedBranchList.length <= 0) {
      this.snackbar.ShowSnackbar("Select branches", "OK");
      return;
    }
    else {
      let postUrl = "";
      let staffData = this.staffForm.value;

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Identity.UpdateUser;
      }
      else {
        postUrl = this.common.apiRoutes.Identity.RegisterUser;
      }

      staffData.branchIds = this.checkedBranchList;

      this.common.post(postUrl, staffData).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.type = "new";
          this.common.NavigateToRoute("configuration/staff");
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

  saveRole() {
    this.roleFormSubmitted = true;
    if (this.roleForm.invalid) {
      return;
    }
    else {
      let postUrl = "";
      let roleData = this.roleForm.value;

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Identity.UpdateRole;
      }
      else {
        postUrl = this.common.apiRoutes.Identity.AddNewRole;
      }

      this.common.post(postUrl, roleData).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.type = "new";
          this.common.NavigateToRoute("configuration/staff");
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

  updateStaff(item: any) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.type = "staff";
        this.staffForm.patchValue(item);
      }
    });

    this.isUpdatingValue = true;
  }

  updateRole(roleId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        var roleDetails = this.rolesList.filter((role: { roleId: string }) => role.roleId = roleId)[0];
        this.roleForm.setValue({
          'roleName': roleDetails.name,
          'id': roleDetails.roleId
        });
        this.type = "role";
        this.isUpdatingValue = true;
      }
    });
  }

  deleteRole(roleId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Identity.DeleteRole + '?roleId=' + roleId).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
            this.new = false;
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

  deleteStaff(userId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Identity.DeleteStaff + '?userId=' + userId).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
            this.new = false;
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

  onBranchListSelectionChange(option: any, event: any) {
    if (event.target.checked) {
      this.checkedBranchList.push(option.branchId);
    } else {
      for (var i = 0; i < this.branchesList.length; i++) {
        if (this.checkedBranchList[i] == option.branchId) {
          this.checkedBranchList.splice(i, 1);
        }
      }
    }
  }

  cancel() {
    this.type = "new";
    this.isUpdatingValue = false;
    this.staffFormSubmitted = false;
    this.roleFormSubmitted = false;
    this.staffForm.reset();
    this.roleForm.reset();
  }

  role() {
    this.type = "role";
  }

  staff() {
    this.type = "staff";
  }
}
