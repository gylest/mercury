//
// CodedValue
//
export class CodedValue {
  groupName:   string;
  value:       string;
  description: string;

    constructor(theGroupName: string, theValue: string, theDescription: string) {
        this.groupName = theGroupName;
        this.value = theValue;
        this.description = theDescription;
    }
}
