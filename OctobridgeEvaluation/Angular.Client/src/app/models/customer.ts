//
// Customer Class
//
export class Customer {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  postalCode: string;
  telephone: string;
  email: string;
  recordCreated: Date | null;
  recordModified: Date | null;

  constructor(
    theId = 0,
    theFirstName = '',
    theLastName = '',
    theMiddleName = '',
    theAddressLine1 = '',
    theAddressLine2 = '',
    theCity = '',
    thePostCode = '',
    theTelephone = '',
    theEmail = '',
    theRecordCreated: Date | null = null,
    theRecordModified: Date | null = null
  ) {
    this.id = theId;
    this.firstName = theFirstName;
    this.lastName = theLastName;
    this.middleName = theMiddleName;
    this.addressLine1 = theAddressLine1;
    this.addressLine2 = theAddressLine2;
    this.city = theCity;
    this.postalCode = thePostCode;
    this.telephone = theTelephone;
    this.email = theEmail;
    this.recordCreated = theRecordCreated;
    this.recordModified = theRecordModified;
  }
}
