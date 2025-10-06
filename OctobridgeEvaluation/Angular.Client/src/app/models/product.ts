//
// Product Class
//
export class Product {
  productId: number;
  name: string;
  productNumber: string;
  productCategoryId: number;
  cost: number;
  recordCreated: Date | null;
  recordModified: Date | null;

  constructor(theProductId = (-1), theName = '', theProductNumber = '', theProductCategoryId = (-1), theCost = 0,
    theRecordCreated: Date | null = null, theRecordModified: Date | null = null) {
    this.productId = theProductId;
    this.name = theName;
    this.productNumber = theProductNumber;
    this.productCategoryId = theProductCategoryId;
    this.cost = theCost;
    this.recordCreated = theRecordCreated;
    this.recordModified = theRecordModified;
  }

}
