//
// Order Class
//
export class Order {
  id: number;
  orderStatus: string;
  customerId: number;
  freightAmount: number;
  subTotal: number;
  totalDue: number;
  paymentDate: Date | null;
  shippedDate: Date | null;
  cancelDate: Date | null;
  recordCreated: Date | null;
  recordModified: Date | null;

  constructor(
    theId = -1,
    theOrderStatus = '',
    theCustomerId = -1,
    theFreightAmount = -1,
    theSubTotal = -1,
    theTotalDue = -1,
    thePaymentDate: Date | null = null,
    theShippedDate: Date | null = null,
    theCancelDate: Date | null = null,
    theRecordCreated: Date | null = null,
    theRecordModified: Date | null = null
  ) {
    this.id = theId;
    this.orderStatus = theOrderStatus;
    this.customerId = theCustomerId;
    this.freightAmount = theFreightAmount;
    this.subTotal = theSubTotal;
    this.totalDue = theTotalDue;
    this.paymentDate = thePaymentDate;
    this.shippedDate = theShippedDate;
    this.cancelDate = theCancelDate;
    this.recordCreated = theRecordCreated;
    this.recordModified = theRecordModified;
  }
}
