import { DatePipe } from '@angular/common';
import { Injector, Pipe, PipeTransform, Type } from '@angular/core';

@Pipe({
  name: 'dynamic'
})
export class DynamicPipe implements PipeTransform {

  constructor(private injector: Injector) { }

  transform(value: unknown, pipeType: Type<any>, args: any): unknown {
    if (!pipeType || typeof pipeType === "string") {
      return value;
    }
    
    const injector = Injector.create({
      name: 'DynamicPipe',
      parent: this.injector,
      providers: [
        { provide: pipeType }
      ]
    });

    const pipe = injector.get(pipeType)
    
    if(pipeType === DatePipe){
      return pipe.transform(value, args, '', 'pt-BR');
    }

    return pipe.transform(value, args);
  }

}
