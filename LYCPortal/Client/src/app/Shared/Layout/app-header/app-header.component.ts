import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/app/Shared/Services/snackbar.service';
import { ClientConstants } from '../../Common/Common';
import { ResponseStatus } from '../../Enums/enum';
import { CommonService } from '../../Services/httpclient.service';
import { DialogsComponent } from '../dialogs/dialogs.component';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent {

  public userName: string = 'User Name';

  constructor(private common: CommonService, private snackbar: SnackbarService, private dialog: MatDialog, private router: Router) {
    this.GetUserByToken();
  }

  GetUserByToken() {
    this.common.get(this.common.apiRoutes.Home.GetUserByToken).subscribe(response => {
      if (response.status == ResponseStatus.OK) {
        localStorage.setItem(ClientConstants.SessionConstants.UserName, '' + response.resultData);
        this.userName = '' + response.resultData;
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
  logout() {
    const dialogRef = this.dialog.open(DialogsComponent, {
      data: { title: 'Logout Confirmation', message: 'Do you want to Logout ?' }
    });
    dialogRef.afterClosed().subscribe(response => {
      if (response) {
      localStorage.clear();

      }
    });
    this.router.navigate(['']);

      }
}
