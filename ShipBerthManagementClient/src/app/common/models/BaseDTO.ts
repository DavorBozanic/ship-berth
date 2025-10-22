export interface BaseDTO {
  id?: number;
  createdByUserId: number;
  createdAt: Date,
  updatedAt?: Date,
}