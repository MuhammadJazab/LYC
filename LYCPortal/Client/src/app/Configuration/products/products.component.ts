import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ClientConstants, ProductStatus } from 'src/app/Shared/Common/Common';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { DialogsComponent } from '../../Shared/Layout/dialogs/dialogs.component';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  public new: boolean = false;
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public branchesList: any = [];
  public productsList: any = [];
  public isUpdatingValue: boolean = false;
  public imageSrc: string = ClientConstants.ImageConstants.DefaultUploadImage;

  selectedProductStatue = 0;
  productStatus = ProductStatus;

  imgResultBeforeCompression: string = "";
  imgResultAfterCompression: string = "";

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private router: Router, private dialog: MatDialog, private imageCompress: NgxImageCompressService) {
    this.appValForm = this.formBuilder.group({
      'productId': [''],
      'branchId': ['', [Validators.required]],
      'productNumber': ['', [Validators.required]],
      'productName': ['', [Validators.required]],
      'productCost': ['', [Validators.required]],
      'displayCost': ['', [Validators.required]],
      'productStatus': ['', [Validators.required]],
      'productImage': ['']
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }

  get f() { return this.appValForm.controls }

  refreshData() {
    this.imageSrc = ClientConstants.ImageConstants.DefaultUploadImage
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.appValForm.reset();
    this.getProductList();
    this.getBranchesList();
  }

  getProductList() {
    this.common.get(this.common.apiRoutes.Configuration.Product.GetProducts).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.productsList = response.resultData;
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
        postUrl = this.common.apiRoutes.Configuration.Product.UpdateProduct;
      }
      else {
        postUrl = this.common.apiRoutes.Configuration.Product.AddNewProduct;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.new = false;
          this.common.NavigateToRoute("configuration/products");
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

  deleteProduct(productId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Product.DeleteProduct + '?productId=' + productId).subscribe(response => {
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

  updateProduct(item: any) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Update Confirmation', message: 'Do you want to update ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.appValForm.patchValue(item);
        this.GetProductImage(item.productId);
        this.new = true;
        this.isUpdatingValue = true;
      }
    });
  }

  GetProductImage(productId: bigint) {
    this.common.get(this.common.apiRoutes.Configuration.Product.GetProductImage + '?productId=' + productId).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.imageSrc = response.resultData + '';
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
    this.isUpdatingValue = false;
    this.formSubmitted = false;
    this.imageSrc = ClientConstants.ImageConstants.DefaultUploadImage;
    this.appValForm.reset();
  }
}
