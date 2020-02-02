import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs/operators';
import { ErrorDialogComponent } from '../common/layouts/error-dialog.component';
import { ColyseusClientService } from '../common/services/colyseus-client.service';

@Component({
  selector: 'app-join-container',
  template: `
    <form class=" d-flex flex-column align-items-center justify-content-center w-100 h-100 join-container"
          [formGroup]="form"
          novalidate>
      <div class="row w-100">
        <div class="col-6 offset-3">
          <div class="form-group text-center">
            <label for="username" class="h5">USERNAME</label>
            <input type="text"
                   class="form-control form-control-lg"
                   [ngClass]="{'is-invalid': form.get('username').touched && form.get('username').errors}"
                   id="username"
                   placeholder="Your Username"
                   formControlName="username">
            <div class="invalid-feedback" *ngIf="form.get('username').touched && form.get('username').errors">
              <small *ngIf="form.get('username').hasError('required')">Username is required</small>
              <small *ngIf="form.get('username').hasError('minlength')">Username must be at least 4 characters</small>
            </div>
          </div>
        </div>
      </div>
      <div class="row w-100">
        <div class="col-6 offset-3">
          <div class="form-group text-center">
            <label for="roomId" class="h5">ROOM ID</label>
            <input type="text"
                   class="form-control form-control-lg"
                   [ngClass]="{'is-invalid': form.get('roomId').touched && form.get('roomId').errors}"
                   id="roomId"
                   placeholder="Enter the room Id"
                   formControlName="roomId">
            <div class="invalid-feedback" *ngIf="form.get('roomId').touched && form.get('roomId').errors">
              <small>RoomId is required</small>
            </div>
          </div>
        </div>
      </div>
      <div class="row w-100 d-flex justify-content-center">
        <h4>JOIN AS</h4>
      </div>
      <div class="row m-0 p-0 w-100">
          <div class="d-flex w-100 join-as-buttons">
            <button class="select-bot-button" (click)="onJoin('a')"><img class="bot-image" src="assets/gif/cubot.gif" alt="">CUBE_BOT</button>
            <button class="select-bot-button" (click)="onJoin('b')"><img class="bot-image" src="assets/gif/spiderbot.gif" alt="">SPIDER_BOT</button>
            <button class="select-bot-button" (click)="onJoin('c')"><img class="bot-image" src="assets/gif/spherebot.gif" alt="">SPHERE_BOT</button>
          </div>
        </div>
    </form>
  `,
  styles: [`
    .join-container {
      background-color: black;
      background-image: url('assets/svg/backPattern.svg');
      width: 100%;
      color: #40ebee;
    }

    .bot-image {
      height: 100px;
      width: 100px;
    }

    .join-as-buttons {
      justify-content: space-evenly;
    }

    .select-bot-button {
      color: #40ebee;
      background-color: transparent;
      border: none;
    }
  `],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class JoinContainerComponent implements OnInit {
  form: FormGroup;
  robotTypes: { label: string, value: string }[] = [
    { label: 'Cube Bot', value: 'a' },
    { label: 'Spidey Bot', value: 'b' },
    { label: 'Sphere Bot', value: 'c' },
    // {label: 'Robot D', value: 'd'},
  ];

  constructor(
    private readonly colyseusClientService: ColyseusClientService,
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly ngbModal: NgbModal
  ) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      roomId: ['', Validators.required],
    });

    this.colyseusClientService.username$().pipe(take(1)).subscribe(val => {
      if (val) {
        this.form.get('username').setValue(val);
      }
    });
  }

  onJoin(type: string = 'b') {
    const { roomId, username } = this.form.value;
    this.colyseusClientService.join(roomId, username, type)
      .subscribe(room => {
        this.colyseusClientService.setRoom(room);
        this.router.navigate(['/controller']);
      }, err => {
        if (!(err instanceof ProgressEvent)) {
          const ref = this.ngbModal.open(ErrorDialogComponent, { centered: true, backdrop: true });
          ref.componentInstance.errorMessage = err.message || 'Meh.';
        }
      });
  }
}
