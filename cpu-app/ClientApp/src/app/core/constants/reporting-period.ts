export const REPORTING_PERIODS = {
    100000000: { name: "1st Quarter", display: "Q1 April 1 - June 30", multiplier: 1 },
    100000001: { name: "2nd Quarter", display: "Q2 July 1 - September 30", multiplier: 2 },
    100000002: { name: "3rd Quarter", display: "Q3 October 1 - December 31", multiplier: 3 },
    100000003: { name: "4th Quarter", display: "Q4 January 1 - March 31", multiplier: 4 },
}

export const PAYMENT_QUARTERS = [
    { month: 3, day: 15, quarter: "Q1" },
    { month: 6, day: 15, quarter: "Q2" },
    { month: 9, day: 15, quarter: "Q3" },
    { month: 0, day: 15, quarter: "Q4" },
]