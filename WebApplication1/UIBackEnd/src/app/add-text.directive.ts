import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appAddText]'
})
export class AddTextDirective {

  constructor(Element: ElementRef) {
    console.log(Element);
    Element.nativeElement.innerText = "Text is added by appAddText Directive.";
  }

}
