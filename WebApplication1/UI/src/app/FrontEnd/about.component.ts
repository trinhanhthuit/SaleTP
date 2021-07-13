import { Component, OnInit } from '@angular/core';
import { map, filter, tap } from 'rxjs/operators'
import { FrontEndService } from 'src/app/Service/frontEndService';
import { AboutView } from 'src/app/Shared/models/product';
declare var $: any;
@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  constructor(
    private frontEndService: FrontEndService
  ) { }
  title = 'Trang chủ';
  langId = 'vi-VN';
  about: AboutView = {};
  htmlToAddSubAbout = '';
  textdata: any = {};
  ngOnInit(): void {
    this.getData();
  }
  getData(): void {
    this.frontEndService.getDataAbout(this.langId).subscribe(res => {
      if (res.IsSuccess) {
        this.about = {
          aboutId: res.Data.About.AboutID,
          aboutTitle: res.Data.About.AboutTitle,
          aboutContent: res.Data.About.AboutContent,
          aboutContent2: res.Data.About.AboutContent2,
          aboutSubContent: res.Data.About.AboutSubContent,
          aboutContent3: res.Data.About.AboutContent3,
        };
        let listSubContent = (<string>this.about.aboutSubContent).split('-');
        for (const item of listSubContent) {
          this.htmlToAddSubAbout += ' <li><i class="ri-check-double-line"></i>' + item + '</li>';
        }
        $('#about-subcontent').append(this.htmlToAddSubAbout);

        this.textdata = {
          textID: res.Data.TextData.TextID,
          langID: res.Data.TextData.LangID,
          title: res.Data.TextData.Title,
          shortContent: res.Data.TextData.ShortContent,
          longContent: res.Data.TextData.LongContent,
          pageCode: res.Data.TextData.PageCode,
        };

      }
    },
      response => {
        console.log("POST call in error", response);
      });
  }
}

