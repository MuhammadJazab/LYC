import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { apiRoutes } from '../ApiRoutes/ApiRoutes';
import { ApiResponse } from '../Interfaces/apiResponse';
import { ClientConstants } from '../Common/Common'
import { FormArray, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class CommonService {
  public baseUrl: any;

  constructor(private httpClient: HttpClient, private router: Router) {
    this.baseUrl = apiRoutes.BaseUrl;
  }

  public apiRoutes = apiRoutes;

  get(api: string) {
    var token = localStorage.getItem(ClientConstants.SessionConstants.AuthSession);
    if (token == null) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': 'Bearer ' + token });
    let options = { headers: headers };
    return this.httpClient.get<ApiResponse>(this.baseUrl + api, options);
  }

  post(api: string, data: any) {
    var token = localStorage.getItem(ClientConstants.SessionConstants.AuthSession);
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': 'Bearer ' + token });
    let options = { headers: headers };
    return this.httpClient
      .post<ApiResponse>(this.baseUrl + api, data, options);
  }

  delete(api: string, data: any) {
    var token = localStorage.getItem(ClientConstants.SessionConstants.AuthSession);
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': 'Bearer ' + token });
    let options = { headers: headers };
    return this.httpClient.delete(this.baseUrl + api + "/" + data.id, options);
  }

  update(api: string, data: any) {
    var token = localStorage.getItem(ClientConstants.SessionConstants.AuthSession);
    if (!token) {
      token = "";
    }
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', 'Access-Control-Allow-Origin': '*', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS', 'Access-Control-Allow-Headers': '*', 'Authorization': 'Bearer ' + token });
    let options = { headers: headers };
    return this.httpClient.put(this.baseUrl + api + "/" + data.id, JSON.stringify(data), options).subscribe(response => {
      if (typeof response == "string") {
        return JSON.parse(response);
      }
      else {
        return response;
      }
    });
  }

  public NavigateToRouteWithQueryString(routeName: string, queryParams?: NavigationExtras) {
    if (queryParams == undefined || queryParams == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName], queryParams);
  }

  public NavigateToRoute(routeName: string, params?: NavigationExtras) {
    if (params == undefined || params == null)
      this.router.navigate([routeName]);
    else
      this.router.navigate([routeName, params]);
  }

  public findInvalidControlsRecursive(formToInvestigate: FormGroup | FormArray): string[] {
    var invalidControls: string[] = [];
    let recursiveFunc = (form: FormGroup | FormArray) => {
      Object.keys(form.controls).forEach(field => {
        const control = form.get(field);
        if (control?.invalid) invalidControls.push(field);
        if (control instanceof FormGroup) {
          recursiveFunc(control);
        } else if (control instanceof FormArray) {
          recursiveFunc(control);
        }
      });
    }
    recursiveFunc(formToInvestigate);
    return invalidControls;
  }
}