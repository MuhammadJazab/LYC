import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/Services/httpclient.service';
import { ResponseStatus } from '../../Shared/Enums/enum';
import { ClientConstants } from '../../Shared/Common/Common';
import { SnackbarService } from '../../Shared/Services/snackbar.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public formSubmitted: boolean = false;

  constructor(private formBuilder: FormBuilder, private common: CommonService, private snackbar: SnackbarService, private router: Router) {
    this.loginForm = this.formBuilder.group({
      'userName': ['', [Validators.required]],
      'password': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.CheckSession();
  }

  get f() { return this.loginForm.controls }

  CheckSession() {
    var session = localStorage.getItem(ClientConstants.SessionConstants.AuthSession);
    if (session != null) {
      this.common.NavigateToRoute("/dashboard")
    }
  }

  submitt() {
    this.formSubmitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    else {
      let data = this.loginForm.value;
      this.common.post(this.common.apiRoutes.Identity.AuthenticateUser, data).subscribe(response => {
        if (response.status == ResponseStatus.OK) {
          localStorage.setItem(ClientConstants.SessionConstants.AuthSession, '' + response.resultData)
          this.common.NavigateToRoute("/dashboard")
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
  }
}
