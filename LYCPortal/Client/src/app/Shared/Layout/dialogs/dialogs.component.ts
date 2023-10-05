import { Component, Inject } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BranchesComponent } from '../../../Configuration/branches/branches.component';

@Component({
  selector: 'app-dialogs',
  templateUrl: './dialogs.component.html',
  styleUrls: ['./dialogs.component.css']
}) 
export class DialogsComponent {

   title: string = 'Confirmation Message';
  message: string = 'Are you sure to perform this action?';
  confirmButtonText = "Yes";
  cancelButtonText = "No";

  constructor(@Inject(MAT_DIALOG_DATA) private data: any, private dialogRef: MatDialogRef<DialogsComponent>) {
    this.title = data.title;
    this.message = data.message;
  }

  onConfirmClick() {
    return true;
    this.dialogRef.close();
  }
}
