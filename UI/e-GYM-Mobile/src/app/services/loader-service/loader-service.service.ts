import { Injectable } from '@angular/core';
import { LoadingController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  public loading;
  public isLoading = false;
  public isLoaded = false;

  constructor(public loadingController: LoadingController) { }

  public async show() {
    this.isLoading = true;
    this.isLoaded = false;

    if (this.loading) {
      this.hide();
    }
    
    this.loading = this.loadingController.create({
      cssClass: '',
      message: 'Carregando...',
    });

    return this.loading.then(a => {
      a.present().then(() => {
        this.isLoaded = true;

        if (!this.isLoading) {
          a.dismiss().then();
        }
      });
    });
  }

  public async hide() {
    this.isLoading = false;

    if (this.isLoaded) {
      return await this.loadingController.dismiss().then();
    }
  }
}