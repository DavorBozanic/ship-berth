import { BaseDTO } from "../../common/models/BaseDTO";

export interface ShipDTO extends BaseDTO {
  name: string;
  size: number;
  type: string;
}