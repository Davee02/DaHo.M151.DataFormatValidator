import { Component, OnInit } from "@angular/core";
import { DataFormat } from "src/app/models/dataFormat";
import { DataSchema } from "src/app/models/dataSchema";
import { SchemaService } from "src/app/services/schema.service";
import { DataFormatService } from "src/app/services/data-format.service";

@Component({
  selector: "app-schema",
  templateUrl: "./schema.component.html",
  styleUrls: ["./schema.component.css"],
})
export class SchemaComponent implements OnInit {
  public allDataFormats: DataFormat[];
  public allSchemas: DataSchema[];

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

  public async deleteSchema(schemaName: string): Promise<void> {
    await this.schemaService.deleteSchema(schemaName).toPromise();

    await this.ngOnInit();
  }

  public async editSchema(): Promise<void> {}
}
