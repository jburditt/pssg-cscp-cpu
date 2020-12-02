import { ccseaMemberTypeDict } from "./ccsea-member-type";

export function decodeCcseaMemberType(code: number): string {
  return ccseaMemberTypeDict[code] || null;
}
