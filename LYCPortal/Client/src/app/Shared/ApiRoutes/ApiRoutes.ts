export var apiRoutes = {
  BaseUrl: 'http://localhost:52416/',
  //BaseUrl: 'http://api.lycmcs.com/',
  Home: {
    GetUserByToken: 'api/Home/GetUserByToken'
  },
  Identity: {
    AuthenticateUser: 'api/IdentityResponsibility/AuthenticateUser',
    GetRoles: 'api/IdentityResponsibility/GetRoles',
    GetAllUsers: 'api/IdentityResponsibility/GetAllUsers',
    AddNewRole: 'api/IdentityResponsibility/AddNewRole',
    RegisterUser: 'api/IdentityResponsibility/RegisterUser',
    DeleteRole: 'api/IdentityResponsibility/DeleteRole',
    DeleteStaff: 'api/IdentityResponsibility/DeleteStaff',
    UpdateRole: 'api/IdentityResponsibility/UpdateRole',
    UpdateUser: 'api/IdentityResponsibility/UpdateUser'
  },

  Customer: {
    GetAllCustomers: 'api/IdentityResponsibility/GetAllCustomers',
    DeleteCustomer: 'api/IdentityResponsibility/DeleteCustomer',
    AddNewCustomer: 'api/IdentityResponsibility/AddNewCustomer',
    UpdateCustomer: 'api/IdentityResponsibility/UpdateCustomer'
  },
  Configuration: {
    Department: {
      GetDepartments: 'api/Department/GetDepartments',
      AddNewDepartment: 'api/Department/AddNewDepartment',
      UpdateDepartment: 'api/Department/UpdateDepartment',
      DeleteDepartment: 'api/Department/DeleteDepartment'
    },
    Branch: {
      AddNewBranch: 'api/Branch/AddNewBranch',
      GetAllBranches: 'api/Branch/GetBranches',
      DeleteBranch: 'api/Branch/DeleteBranch',
      UpdateBranch: 'api/Branch/UpdateBranch',
      GetBrancheByRegistrationNumber: 'api/Branch/GetBranchByRegistrationNumber',
      GetBranchFacilitiesByBranchId: 'api/Branch/GetBranchFacilitiesByBranchId'
    },
    Room: {
      GetRooms: 'api/Room/GetRooms',
      AddNewRoom: 'api/Room/AddNewRoom',
      UpdateRoom: 'api/Room/UpdateRoom',
      DeleteRoom: 'api/Room/DeleteRoom',
    },
    Product: {
      AddNewProduct: 'api/Product/AddNewProduct',
      GetProducts: 'api/Product/GetProducts',
      GetProductImage: 'api/Product/GetProductImage',
      DeleteProduct: 'api/Product/DeleteProduct',
      UpdateProduct: 'api/Product/UpdateProduct'
    },
    Package: {
      AddNewPackage: 'api/Package/AddNewPackage',
      GetPackages: 'api/Package/GetPackages',
      DeletePackage: 'api/Package/DeletePackage',
      UpdatePackage: 'api/Package/UpdatePackage'
    },
    Promotion: {
      AddNewPromotion: 'api/Promotion/AddNewPromotion',
      GetPromotions: 'api/Promotion/GetPromotions',
      DeletePromotion: 'api/Promotion/DeletePromotion',
      UpdatePromotion: 'api/Promotion/UpdatePromotion'
    },
    Service: {
      AddNewService: 'api/Service/AddNewService',
      GetServices: 'api/Service/GetServices',
      DeleteService: 'api/Service/DeleteService',
      UpdateService: 'api/Service/UpdateService'
    }
  },
  Finance: {
    GetFinances: 'api/Finance/GetFinances',
    AddNewFinance: 'api/Finance/AddNewFinance',
    DeleteFinance: 'api/Finance/DeleteFinance',
  }
}
