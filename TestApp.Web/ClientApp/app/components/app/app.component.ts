import { Component, Inject } from '@angular/core';
import { JL } from 'jsnlog';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    JL: JL.JSNLog

    constructor(
        @Inject('JSNLOG') JL: JL.JSNLog) {

        this.JL = JL;
        this.JL().error("Testing purposes");
    }
}
