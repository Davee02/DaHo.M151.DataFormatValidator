import { Component, OnInit } from "@angular/core";
import { DataFormatService } from "src/app/services/data-format.service";
import { ValidateFormatRequest } from "src/app/models/validateFormatRequest";
import { DataFormat } from "src/app/models/dataFormat";
import { ValidateFormatResponse } from "src/app/models/validateFormatResponse";
import { SchemaService } from "src/app/services/schema.service";
import { DataSchema } from "src/app/models/dataSchema";

@Component({
  selector: "app-validate",
  templateUrl: "./validate.component.html",
  styleUrls: ["./validate.component.css"],
})
export class ValidateComponent implements OnInit {
  public allDataFormats: DataFormat[];
  public allSchemas: DataSchema[];

  public selectedDataFormat: string;
  public selectedSchema: string;
  public withSchema: boolean;
  public content: string;
  public validateResult: ValidateFormatResponse;

  constructor(
    private dataFormatService: DataFormatService,
    private schemaService: SchemaService
  ) {}

  public async ngOnInit(): Promise<void> {
    this.allDataFormats = await this.dataFormatService
      .getAllDataFormats()
      .toPromise();
    this.allSchemas = await this.schemaService.getAllSchemas().toPromise();
  }

  public async validateContent(): Promise<void> {
    const requestData: ValidateFormatRequest = {
      format: this.selectedDataFormat,
      content: this.content,
    };

    if (this.withSchema) {
      this.validateResult = await this.dataFormatService
        .validateDataFormatWithSchema(requestData, this.selectedSchema)
        .toPromise();
    } else {
      this.validateResult = await this.dataFormatService
        .validateDataFormat(requestData)
        .toPromise();
    }
  }
}
