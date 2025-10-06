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

  constructor(theLineId: number = (-1), theOrderId: number = (-1), theProductId: number = (-1), theUnitPrice: number = (-1),
    theQuantity: number = (-1), theRecordCreated: Date | null = null, theRecordModified: Date | null = null) {
    this.lineId = theLineId;
    this.orderId = theOrderId;
    this.productId = theProductId;
    this.unitPrice = theUnitPrice;
    this.quantity = theQuantity;
    this.recordCreated = theRecordCreated;
    this.recordModified = theRecordModified;
  }

}
