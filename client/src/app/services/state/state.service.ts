import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { MessageLevel } from 'src/app/enums/message-level';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  public showLogout: BehaviorSubject<boolean> = new BehaviorSubject(false);
  public loadingScreen: BehaviorSubject<any> = new BehaviorSubject({display: false, message: 'Please wait...'});
  public appMessage: BehaviorSubject<any> = new BehaviorSubject({display: false, level: MessageLevel.error, message: ""})

  constructor() { }


  hideLoadingScreen() {
    this.loadingScreen.next({display: false, message: 'Please wait...'});
  }

  showLoadingScreen(message?: string) {
    if(!message) {
      message = 'Please wait...';
    }
    this.loadingScreen.next({display: true, message: message});
  }
  
  showAppMessage(level: MessageLevel, message: string){
    this.appMessage.next({display: true, level: level, message: message});
    
    setTimeout(() => {
      this.hideAppMessage();
    }, 5000);
  }

  hideAppMessage(){
    this.appMessage.next({display: false, level: MessageLevel.error, message: ""});
  }
}
