export const usernameRegex: string = '[a-zA-Z0-9_.]{0,20}';
export const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
export const passwordRegex: RegExp = /(?=.*[a-z]+.*)(?=.*[A-Z]+.*)(?=.*[0-9]+.*)(?=.{8,})(^(?!.*\s).+$)/;