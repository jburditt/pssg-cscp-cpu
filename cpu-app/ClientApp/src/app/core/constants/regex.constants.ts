export const POSTAL_CODE: RegExp = /^[A-Za-z][0-9][A-Za-z][ ]?[0-9][A-Za-z][0-9] *$/;
export const WORD: RegExp = /\w{2,}/; // there must be at least 2 valid characters
export const EMAIL: RegExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})) *$/;
export const PHONE_NUMBER: RegExp = /^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/;
export const LETTERS_SPACES: RegExp = /^[a-zA-Z|\s]{2,}$/;
export const NAME_REGEX: RegExp = /^[a-zA-Z|\s'.-]{2,}$/;
export const TIME: RegExp = /^(0?[1-9]|1[0-2])[0-5][0-9]$/;
