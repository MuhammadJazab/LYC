import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ClientConstants } from '../../Shared/Common/Common';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { DialogsComponent } from '../../Shared/Layout/dialogs/dialogs.component';
import { CommonService } from '../../Shared/Services/httpclient.service';

@Component({
  selector: 'app-promotion',
  templateUrl: './promotion.component.html',
  styleUrls: ['./promotion.component.css']
})

export class PromotionComponent implements OnInit {
  public appValForm: FormGroup;
  public formSubmitted: boolean = false;
  public promotionsList: any = [];
  public serviceList: any = [];
  public productsList: any = [];
  public isUpdatingValue: boolean = false;
  public isPercentSelected: boolean = true;
  public isProductSelected: boolean = true;

  constructor(private modalService: NgbModal, private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.appValForm = this.formBuilder.group({
      'promotionId': [''],
      'productId': [''],
      'serviceId': [''],
      'promotionName': ['', [Validators.required]],
      'discount': ['', [Validators.required]],
      'expiryDate': ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.refreshData();
  }

  get f() { return this.appValForm.controls }

  refreshData() {
    this.formSubmitted = false;
    this.getPromotionList();
    this.appValForm.reset();
    this.getServiceList();
    this.getProductList();
  }

  open(content: any) {
    this.modalService.open(content);
  }

  getPromotionList() {
    this.common.get(this.common.apiRoutes.Configuration.Promotion.GetPromotions).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.promotionsList = response.resultData;
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

    if (this.isPercentSelected) {
      this.appValForm.value.discountType = 1;
    }
    else {
      this.appValForm.value.discountType = 0;
    }

    if (this.appValForm.invalid) {
      return;
    }
    else {
      let data = this.appValForm.value;
      let postUrl = "";

      if (this.isUpdatingValue) {
        postUrl = this.common.apiRoutes.Configuration.Promotion.UpdatePromotion;
      }
      else {
        postUrl = this.common.apiRoutes.Configuration.Promotion.AddNewPromotion;
      }

      this.common.post(postUrl, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          this.modalService.dismissAll();
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
        this.snackbar.ShowSnackbar(exception.name, "OK");
      });
    }
  }

  deletePromotion(promotionId: string) {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Delete Confirmation', message: 'Do you want to delete ?' }
    });

    dialogRef.afterClosed().subscribe(response => {
      if (response) {
        this.common.get(this.common.apiRoutes.Configuration.Promotion.DeletePromotion + '?promotionId=' + promotionId).subscribe(response => {
          if (response.status == ResponseStatus.OK) {
            this.refreshData();
          }
          else {
            this.snackbar.ShowSnackbar(response.message, "OK");
          }
        }, exception => {
          this.snackbar.ShowSnackbar(exception.name, "OK");
        });
      }
    });
  }

  updatePromotion(item: any, content: any) {
    const dialogref = this.dialog.open(DialogsComponent, {
      data: { title: 'Update confirmation', message: 'Do you want to update ?' }
    });

    dialogref.afterClosed().subscribe(response => {
      this.refreshData();
      if (response) {
        if (item.productId != null)
          this.isProductSelected = true

        if (item.serviceId != null)
          this.isProductSelected == false;

        this.modalService.open(content);
        this.appValForm.patchValue(item);

        this.isUpdatingValue = true;
      }
    });

  }

  onChangeRadioButton(isChanged: boolean) {
    this.isProductSelected = isChanged;
  }

  onChangeDiscountType(isChange: boolean) {
    this.isPercentSelected = isChange;
  }

  cancel() {
    this.modalService.dismissAll();
    this.isUpdatingValue = false;
    this.appValForm.reset();
  }
}
