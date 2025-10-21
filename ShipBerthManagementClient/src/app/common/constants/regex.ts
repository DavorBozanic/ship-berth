export const usernameRegex: RegExp = /^[a-zA-Z0-9_.]+$/;
export const emailRegex: RegExp = /^(?!\s)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?<!\s)$/;
export const passwordRegex: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-={}[\]:;"'<>,.?/~`|\\]).+$/;
export const shipSizeRegex: RegExp = /^(100(\.0)?|([5-9](\.[0-9])?|[1-9][0-9](\.[0-9])?))$/;
;
