import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import Swal from 'sweetalert2';
import { IFormBase } from '../models/CrudBase';

@Injectable({
  providedIn: 'root'
})
export class CancelGuard implements CanDeactivate<IFormBase> {
  async canDeactivate(entity: IFormBase): Promise<boolean> {
    let can: boolean = true;

    if (entity.formEntity.dirty) {
      await Swal.fire({
        title: 'Tem certeza que deseja cancelar as alterações?',
        text: 'Todos os dados modificados serão perdidos!',
        icon: 'warning',
        showCancelButton: true,
        customClass: {
          cancelButton: 'bg-translucent-default',
          confirmButton: 'bg-warning'
        },
        confirmButtonText: 'Sim, cancelar alterações!',
        cancelButtonText: 'Não, continuar as alterações'
      }).then((result) => {
        if (result.value) {
          can = true;
          return;
        }

        can = false;
        return;
      });

      return can;
    } else {
      return true;
    }
  }
}