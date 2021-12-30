import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'apm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  host: {
    class: 'apm-root'
  },
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {

}
