import { BaseDTO } from "../../common/models/BaseDTO";
import { BerthStatus } from "../enums/berth-status";

export interface BerthDTO extends BaseDTO {
  name: string;
  location: string;
  maxShipSize: number;
  status: BerthStatus;
}