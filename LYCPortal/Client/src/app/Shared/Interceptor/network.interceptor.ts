import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoaderService } from '../Services/loader.service';

@Injectable()
export class NetworkInterceptor implements HttpInterceptor {
  totalRequests = 0;
  completedRequests = 0;

  constructor(private loader: LoaderService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loader.showLoader();
    this.totalRequests++;

    return next.handle(request).pipe(finalize(() => {
      this.completedRequests++;
      console.log(this.completedRequests, this.totalRequests);

      if (this.completedRequests === this.totalRequests) {
        this.loader.hideLoader();
        this.completedRequests = 0;
        this.totalRequests = 0;
      }
    }));
  }
}
