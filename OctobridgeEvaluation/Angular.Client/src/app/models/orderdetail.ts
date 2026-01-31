//
// OrderDetail Class
//
export class OrderDetail {
  lineId: number;
  orderId: number;
  productId: number;
  unitPrice: number;
  quantity: number;
  recordCreated: Date | null;
  recordModified: Date | null;

  constructor(
    theLineId = -1,
    theOrderId = -1,
    theProductId = -1,
    theUnitPrice = -1,
    theQuantity = -1,
    theRecordCreated: Date | null = null,
    theRecordModified: Date | null = null
  ) {
    this.lineId = theLineId;
    this.orderId = theOrderId;
    this.productId = theProductId;
    this.unitPrice = theUnitPrice;
    this.quantity = theQuantity;
    this.recordCreated = theRecordCreated;
    this.recordModified = theRecordModified;
  }
}
