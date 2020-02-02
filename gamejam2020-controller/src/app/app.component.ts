import { Component, OnInit } from '@angular/core';
import { ColyseusClientService } from './common/services/colyseus-client.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'gamejam2020-controller';

  constructor(private readonly colyseusClientService: ColyseusClientService) {
  }

  ngOnInit() {
    // this.colyseusClientService.create();
  }
}
