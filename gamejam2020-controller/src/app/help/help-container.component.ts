import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-help-container',
  template: `
    <p>
      help-container works!
    </p>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HelpContainerComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
