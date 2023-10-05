import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private snackbar: MatSnackBar, private zone: NgZone) { }

  ShowSnackbar(message: string, action: string): void {
    this.zone.run(() => {
      this.snackbar.open(message, "✖", {
        horizontalPosition : this.horizontalPosition,
        verticalPosition : this.verticalPosition,
        duration : 5000
      });
    });
  }
}
