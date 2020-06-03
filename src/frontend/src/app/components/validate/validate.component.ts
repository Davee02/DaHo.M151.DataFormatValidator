import { Component, OnInit } from "@angular/core";
import { DataFormatService } from "src/app/services/data-format.service";
import { ValidateFormatRequest } from "src/app/models/validateFormatRequest";
import { DataFormat } from "src/app/models/dataFormat";
import { ValidateFormatResponse } from "src/app/models/validateFormatResponse";

@Component({
  selector: "app-validate",
  templateUrl: "./validate.component.html",
  styleUrls: ["./validate.component.css"],
})
export class ValidateComponent implements OnInit {
  public allDataFormats: DataFormat[];
  public selectedDataFormat: string;
  public content: string;
  public validateResult: ValidateFormatResponse;

  constructor(private dataFormatService: DataFormatService) {}

  public async ngOnInit(): Promise<void> {
    this.allDataFormats = await this.dataFormatService
      .getAllDataFormats()
      .toPromise();
  }

  public async validateContent(): Promise<void> {
    const requestData: ValidateFormatRequest = {
      format: this.selectedDataFormat,
      content: this.content,
    };

    try {
      const response = await this.dataFormatService
        .validateDataFormat(requestData)
        .toPromise();
      this.validateResult = response;
    } catch (error) {
      console.warn(error);
    }
  }
}
