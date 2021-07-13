export interface ProductView {
  productId: string;
  linkCode: string;
  productCode: string;
  productName?: string;
  salePrice?: number;
  discountPrice?: number;
  createdBy?: string;
  createdDate?: Date;
  modifiBy?: string;
  modifyDate?: Date;
  isHot?: boolean;
  isActive?: boolean;
  imagePath?: string;
  categoryCode?: string;
  categoryID?: number;
  parentCode?: string;
  categoryName?: string;
}

export interface CategoryView {
  categoryId:number;
  categoryCode: string;
  categoryName?: string;
 
}

export interface AboutView {
  aboutId?: string;
  aboutTitle?: string;
  aboutContent?: string;
  aboutContent2?: string;
  aboutSubContent?: string;
  aboutContent3?: string;
}
export interface ServiceView {
  
  serviceId: string;
  serviceName?: string;
  title?: string;
  shortContent?: string;
  longContent?: string;
  imagePath1?: string;
  imagePath2?: string;
  linkCode?: string;
}

export interface ImageView {
  imageID: string;
  imagePath: string;
}

export interface TestimonialView {
  testimonialID: string;
  imagePath: string;
  customerName: string;
  position: string;
  content: string;
  subContent: string;
}

export interface EmployeeView {
  employeeID: number;
  imagePath1: string;
  imagePath2: string;
  linkTwitter: string;
  linkFacebook: string;
  linkInstagram: string;
  linkLinkedin: string;
  employeeName: string;
  position: string;
  title: string;
  content: string;
  isActive: boolean;
}
export interface WhyUsView {
  id?: number;
  imagePath1?: string;
  imagePath2?: string;
  sortOrder?: number;
  title?: string;
  subContent?: string;
  content?: string;
  isActive?: boolean;
}
export interface WhyUsDetailView {
  whyUSDetailID: number;
  imagePath1: string;
  imagePath2: string;
  sortOrder: number;
  title: string;
  subContent: string;
  content: string;
  isActive: boolean;
}
export interface TextData {
  textID: number;
  langID: string;
  title: string;
  shortContent: string;
  longContent: string;
  pageCode: string;
}
export interface ContactView {
  contactID: number;
  subject: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  contents: string;
}
export interface MenuView {
  menuCode?: string;
  menuName?: string;
  submenu?: any[];
}
export interface Cart {
  productId: string,
  productCode: string;
  productName: string;
  categoryId: number;
  categoryName: string;
  price:number,
  imagePath: string;
  quantity: number;
  totalPrice: number;
}
export interface AddToCart {
  quantity: number;
  carts:Cart[]
}


