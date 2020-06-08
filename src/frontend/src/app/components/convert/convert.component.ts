import { Component, OnInit } from "@angular/core";
import { DataFormat } from "src/app/models/dataFormat";
import { DataFormatService } from "src/app/services/data-format.service";
import { ConvertFormatRequest } from 'src/app/models/convertFormatRequest';
import { ConvertFormatResponse } from 'src/app/models/convertFormatResponse';

@Component({
  selector: "app-convert",
  templateUrl: "./convert.component.html",
  styleUrls: ["./convert.component.css"],
})
export class ConvertComponent implements OnInit {
  public allDataFormats: DataFormat[];
  public sourceDataFormat: string;
  public destinationDataFormat: string;
  public content: string;
  public convertResult: ConvertFormatResponse;

  constructor(private dataFormatService: DataFormatService) {}

  public async ngOnInit(): Promise<void> {
    this.allDataFormats = await this.dataFormatService
      .getAllDataFormats()
      .toPromise();
  }

  public async convertContent(): Promise<void> {
    const requestData: ConvertFormatRequest = {
      from: this.sourceDataFormat,
      to: this.destinationDataFormat,
      content: this.content,
    };

    try {
      const response = await this.dataFormatService
        .convertDataFormat(requestData)
        .toPromise();
      this.convertResult = response;
    } catch (error) {
    }
  }
}
