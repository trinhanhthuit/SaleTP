import { async,TestBed, ComponentFixture } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
describe('AppComponent', () => {
  let comp:AppComponent;
  let fixture:ComponentFixture<AppComponent>;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent
      ],
    }).compileComponents().then(()=>{
      fixture = TestBed.createComponent(AppComponent);
      comp=fixture.componentInstance;
    });
  });

  it('should create the app', () => {
    expect(comp).toBeTruthy();
  });

  it(`should have as title 'UI'`, () => {
    expect(comp.title).toEqual('UI');
  });
});
