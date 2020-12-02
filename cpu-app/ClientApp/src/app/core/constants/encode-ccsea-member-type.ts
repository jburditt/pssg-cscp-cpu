import { ccseaMemberTypeDict } from "./ccsea-member-type";

export function encodeCcseaMemberType(description: string): number {
  const reverseDict = {};
  for (let property in ccseaMemberTypeDict) {
    if (ccseaMemberTypeDict.hasOwnProperty(property)) {
      reverseDict[ccseaMemberTypeDict[property]] = property;
    }
  }
  return parseInt(reverseDict[description]) || null;
}
