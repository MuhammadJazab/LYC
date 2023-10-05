import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ClientConstants, PackageType } from 'src/app/Shared/Common/Common';
import { DialogsComponent } from 'src/app/Shared/Layout/dialogs/dialogs.component';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  public new: boolean = false;
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public customersList: any = [];
  public branchesList: any = [];
  public isUpdatingValue: boolean = false;
  public imageSrc: string = ClientConstants.ImageConstants.DefaultUploadImage;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router, private imageCompress: NgxImageCompressService) {
    this.appValForm = this.formBuilder.group({
      'customerId': [''],
      'branchId': ['', [Validators.required]],
      'customerName': ['', [Validators.required]],
      'passport': ['', [Validators.required]],
      'customerDob': ['', [Validators.required]],
      'customerContact': ['', [Validators.required]],
      'bloodType': ['', [Validators.required]],
      'customerEmail': ['', [Validators.required]],
      'customerAddress': ['', [Validators.required]],
      'postCode': ['', [Validators.required]],
      'state': ['', [Validators.required]],
      'country': ['', [Validators.required]],
      'customerImg': ['']
    });
  }
  refreshData() {
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
    this.getCustomersList();
    this.getBranchesList();
  }
  ngOnInit(): void {
    this.refreshData();
  }

  onFileChange(event: any) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.imageSrc = reader.result as string;

        this.appValForm.patchValue({
          productImage: reader.result
        });
      };
    }
  }

  get f() { return this.appValForm.controls }

  getCustomersList() {
    this.common.get(this.common.apiRoutes.Customer.GetAllCustomers).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.customersList = response.resultData;
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

  deleteCustomer(customerId: bigint) {
    this.common.get(this.common.apiRoutes.Customer.DeleteCustomer + '?customerId=' + customerId).subscribe(response => {
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

  save() {
    this.formSubmitted = true;
    if (this.appValForm.invalid) {
      return;
    }
    else {
      let data = this.appValForm.value;
      let postUrl = "";

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Customer.UpdateCustomer;
      }
      else {
        postUrl = this.common.apiRoutes.Customer.AddNewCustomer;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("customers");
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

  uploadFile() {
    const MAX_MEGABYTE = 2;
    this.imageCompress.uploadAndGetImageWithMaxSize(MAX_MEGABYTE) // this function can provide debug information using (MAX_MEGABYTE,true) parameters
      .then(
        (result: string) => {
          this.imageSrc = result;
        },
        (result: string) => {
          console.error('The compression algorithm didn\'t succed! The best size we can do is', this.imageCompress.byteCount(result), 'bytes')
          this.imageSrc = result;
        });
  }

  cancel() {
    this.new = false;
    this.formSubmitted = false;
    this.imageSrc = ClientConstants.ImageConstants.DefaultUploadImage;
    this.appValForm.reset();

  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { name: 'Test1', from: 'Component' }
    });

    dialogRef.afterClosed().subscribe((confirmed: any) => {
      console.log(confirmed)
    });
  }
}
