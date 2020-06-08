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

  public selectedSchema: DataSchema;
  public isNewSchema: boolean;
  public errorMessage;

  constructor(
    private dataFormatService: DataFormatService,
    private schemaService: SchemaService
  ) {}

  public async ngOnInit(): Promise<void> {
    try {
      this.allDataFormats = await this.dataFormatService
        .getAllDataFormats()
        .toPromise();
      this.allSchemas = await this.schemaService.getAllSchemas().toPromise();
    } catch (error) {
      this.errorMessage =
        "Loading all schemas failed! Check the developer console for more information";
    }
  }

  public async deleteSchema(schemaName: string): Promise<void> {
    try {
      await this.schemaService.deleteSchema(schemaName).toPromise();
    } catch (error) {
      this.errorMessage =
        "Deleting the schema failed! Check the developer console for more information";
    }

    await this.ngOnInit();
  }

  public editSchema(dataSchema: DataSchema): void {
    this.isNewSchema = false;
    this.selectedSchema = dataSchema;
  }

  public addSchema(): void {
    this.isNewSchema = true;
    const newSchema = { name: "New schema", schema: "", forFormat: "" };
    this.allSchemas.push(newSchema);
    this.selectedSchema = newSchema;
  }

  public async save(): Promise<void> {
    try {
      if (this.isNewSchema) {
        await this.schemaService.createSchema(this.selectedSchema).toPromise();
      } else {
        await this.schemaService
          .editSchema(this.selectedSchema, this.selectedSchema.name)
          .toPromise();
      }
    } catch (error) {
      this.errorMessage =
        "Saving the schema failed! Check the developer console for more information";
    }

    this.selectedSchema = undefined;
  }
}
