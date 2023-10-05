import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FinanceType, FinanceItemType } from '../../Shared/Common/Common';
import { EnumFinanceItemType, EnumFinanceType, ResponseStatus } from '../../Shared/Enums/enum';
import { CommonService } from '../../Shared/Services/httpclient.service';
import { SnackbarService } from '../../Shared/Services/snackbar.service';

@Component({
  selector: 'app-finance',
  templateUrl: './finance.component.html',
  styleUrls: ['./finance.component.css']
})
export class FinanceComponent implements OnInit {
  public userList: any = [];
  public finaanceList: any = [];
  public formSubmitted: boolean = false;
  public financeTypeList: any = [];
  public financeItemTypeList: any = [];
  public servicesList: any = [];
  public appValForm: FormGroup;
  public financeType = EnumFinanceType;
  constructor(private modalService: NgbModal, private common: CommonService, private snackbar: SnackbarService, private formBuilder: FormBuilder) {
    this.appValForm = this.formBuilder.group({
      financeId: [0],
      userId: [null],
      accountsEntry: ['', [Validators.required]],
      itemId: ['', [Validators.required]],
      itemType: [null],
      qty: ['', [Validators.required]],
      cost: ['', [Validators.required]]
    });
  }

  refreshData() {
    this.formSubmitted = false;
    this.appValForm.reset();
    this.getFinanceList();
    this.getCustomerList();
  }

  ngOnInit(): void {
    this.refreshData();
    this.getFinanceList();
    this.getCustomerList();
    this.financeTypeList = FinanceType;
    this.financeItemTypeList = FinanceItemType;
  }
  get f() { return this.appValForm.controls }

  getFinanceList() {
    this.common.get(this.common.apiRoutes.Finance.GetFinances).subscribe(response => {
      if (response.status = ResponseStatus.OK) {
        this.finaanceList = response.resultData;
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK");
      }
    });
  }


  open(content: any) {
    this.modalService.open(content);
  }

  getCustomerList() {
    this.common.get(this.common.apiRoutes.Identity.GetAllUsers).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        this.userList = response.resultData;
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK");
      }
    });
  }

  selectedItem(itemId: any) {
    if (itemId && itemId == EnumFinanceItemType.Services) {
      this.common.get(this.common.apiRoutes.Configuration.Service.GetServices).subscribe(response => {
        if (response.status = ResponseStatus.OK) {
          this.servicesList = response.resultData;
        }
        else {
          this.snackbar.ShowSnackbar(response.message, "OK");
        }
      });
    }
  }

  save() {
    this.formSubmitted = true;
    let data = this.appValForm.value;
    this.common.post(this.common.apiRoutes.Finance.AddNewFinance, data).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        let res = response.message;
        this.modalService.dismissAll();
        this.getFinanceList();
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK");
      }
    });
  }

  delete(financeId: any) {
    this.common.get(this.common.apiRoutes.Finance.DeleteFinance + "?financeId=" + financeId).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        let data = response.resultData;
        this.getFinanceList();
      }
      else {
        this.snackbar.ShowSnackbar(response.message, "OK");
      }
    });
  }

  update(item: any, content: any) {
    this.modalService.open(content);
    this.appValForm.patchValue(item);
  }
  cancel() {
    this.formSubmitted = false;
    this.appValForm.reset();
  }

}
