import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ClientConstants, PackageType } from 'src/app/Shared/Common/Common';
import { DialogsComponent } from 'src/app/Shared/Layout/dialogs/dialogs.component';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.css']
})
export class PackageComponent implements OnInit {
  public new: boolean = false;
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public packagesList: any = [];
  public serviceList: any = [];


  selectedPackageType = 0;

  packageType = PackageType;
  public isUpdatingValue: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.appValForm = this.formBuilder.group({
      'packageId': [''],
      'serviceId': [''],
      'packageNumber': ['', [Validators.required]],
      'packageName': ['', [Validators.required]],
      'packageType': ['', [Validators.required]],
      'packageCost': ['', [Validators.required]],
      'stayPeriodDays': ['', [Validators.required]],
      'serviceQty': ['']
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData() {
    this.getPackagesList();
    this.getServiceList();
    this.appValForm.reset();
  }
  get f() { return this.appValForm.controls }

  getPackagesList() {
    this.common.get(this.common.apiRoutes.Configuration.Package.GetPackages).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.packagesList = response.resultData;
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

  save() {
    this.formSubmitted = true;
    if (this.appValForm.invalid) {
      return;
    }
    else {
      let data = this.appValForm.value;
      let postUrl = "";
      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Package.UpdatePackage;


      }
      else {
        data.packageId = 0;
        postUrl = this.common.apiRoutes.Configuration.Package.AddNewPackage;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("configuration/packages");
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

  updatePackage(item: any) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });
    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.isUpdatingValue = true;
        this.appValForm.patchValue(item);
        this.new = true;
      }
    });
  }

  deletePackage(packageId: bigint) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Package.DeletePackage + '?packageId=' + packageId).subscribe(response => {
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

  cancel() {
    this.new = false;
    this.isUpdatingValue = false;
  }
}
