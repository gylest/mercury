//
// Attachment Class
//
export class Attachment {
  id:         number;
  fileName:   string;
  fileType:   string;
  length:     number;
  recordCreated: Date;

  constructor(theId = 0, theFileName: string, theFileType: string, theLength: number, theRecordCreate:  Date = new Date()) {
    this.id = theId;
    this.fileName = theFileName;
    this.fileType = theFileType;
    this.length = theLength;
    this.recordCreated = theRecordCreate;
  }
}
