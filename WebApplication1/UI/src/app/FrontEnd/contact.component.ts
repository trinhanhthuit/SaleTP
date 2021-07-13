import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators'
import { FrontEndService } from 'src/app/Service/frontEndService';
import { AboutView } from 'src/app/Shared/models/product';
declare var $: any;
@Component({
  selector: 'app-about',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  constructor(
    private frontEndService: FrontEndService
  ) { }
  title = 'Trang chủ';
  langId = 'vi-VN';
  contact: any = {
    fullName: 'Anh Thu',
    email: 'trinhanhthu@gmail.com',
    phoneNumber: '03215682588',
    contents: 'Xin chào',
    subject:'xin chào'
  };
  modal = { isShowModalConfirm: false };
  htmlToAddSubAbout = '';
  textdata: any = {};
  ngOnInit(): void {
    this.getData();
  }
  getData(): void {
    //this.frontEndService.getDataAbout(this.langId).subscribe(res => {
    //  if (res.IsSuccess) {
    //    this.about = {
    //      aboutId: res.Data.About.AboutID,
    //      aboutTitle: res.Data.About.AboutTitle,
    //      aboutContent: res.Data.About.AboutContent,
    //      aboutContent2: res.Data.About.AboutContent2,
    //      aboutSubContent: res.Data.About.AboutSubContent,
    //      aboutContent3: res.Data.About.AboutContent3,
    //    };
    //    let listSubContent = (<string>this.about.aboutSubContent).split('-');
    //    for (const item of listSubContent) {
    //      this.htmlToAddSubAbout += ' <li><i class="ri-check-double-line"></i>' + item + '</li>';
    //    }
    //    $('#about-subcontent').append(this.htmlToAddSubAbout);

    //    this.textdata = {
    //      textID: res.Data.TextData.TextID,
    //      langID: res.Data.TextData.LangID,
    //      title: res.Data.TextData.Title,
    //      shortContent: res.Data.TextData.ShortContent,
    //      longContent: res.Data.TextData.LongContent,
    //      pageCode: res.Data.TextData.PageCode,
    //    };

    //  }
    //},
    //  response => {
    //    console.log("POST call in error", response);
    //  });
  }
  onSaveContact(): void {
    let contact = this.contact;
    let strContact = JSON.stringify(this.contact);
    this.frontEndService.onSaveContact(contact).subscribe(res => {
      if (res.IsSuccess) {
        this.contact = {
          fullName: '',
          email: '',
          phoneNumber: '',
          contents: '',
          subject: ''
        };
        this.showModal();
        //let listSubContent = (<string>this.about.aboutSubContent).split('-');
        //for (const item of listSubContent) {
        //  this.htmlToAddSubAbout += ' <li><i class="ri-check-double-line"></i>' + item + '</li>';
        //}
        //$('#about-subcontent').append(this.htmlToAddSubAbout);

        //this.textdata = {
        //  textID: res.Data.TextData.TextID,
        //  langID: res.Data.TextData.LangID,
        //  title: res.Data.TextData.Title,
        //  shortContent: res.Data.TextData.ShortContent,
        //  longContent: res.Data.TextData.LongContent,
        //  pageCode: res.Data.TextData.PageCode,
        //};

      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }

  showModal(): void {
    $('#exampleModal').modal();
  }
}

