import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-error-dialog',
  template: `
    <div class="modal-header">
      <h4 class="modal-title">Oops, something went wrong</h4>
      <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss()">
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
    <div class="modal-body">
      <p>{{errorMessage}}</p>
    </div>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ErrorDialogComponent implements OnInit {
  @Input() errorMessage: string = '';

  constructor(public activeModal: NgbActiveModal) {
  }

  ngOnInit(): void {
  }

}
