export var ClientConstants = {
  SessionConstants: {
    AuthSession: 'AuthSession',
    UserName: 'UserName',
    ExpiredToken: 'expiredtoken',
    ExpiredTokenMessage: 'expiredtokenmessage'
  },
  Messages: {
    GenericError: 'Try agian or contact administrator.'
  },
  ImageConstants: {
    DefaultUploadImage: '../../../assets/svgs/image-not-found-icon.svg'
  }
}

export var RoomStatus = [{ id: 1, value: 'Active' }, { id: 2, value: 'Occupied' }, { id: 3, value: 'Suspended' }, { id: 4, value: 'Maintenance' }];
export var ProductStatus = [{ id: 1, value: 'Active' }, { id: 2, value: 'Sold Out' }, { id: 3, value: 'Suspended' }, { id: 4, value: 'Removed' }];
export var ServiceStatus = [{ id: 1, value: 'Active' }, { id: 2, value: 'Suspended' }, { id: 3, value: 'Removed' }];
export var RoomType = [{ id: 1, value: 'Deluxe' }, { id: 2, value: 'VIP' }, { id: 3, value: 'Standarad' }];
export var DaysOfWeek = [{ id: 1, value: 'Monday' }, { id: 2, value: 'Tuesday' }, { id: 3, value: 'Wednesday' }, { id: 4, value: 'Thurday' }, { id: 5, value: 'Friday' }, { id: 6, value: 'Saturday' }, { id: 7, value: 'Sunday' }];
export var PackageType = [{ id: 1, value: 'Deluxe' }, { id: 2, value: 'VIP' }, { id: 3, value: 'Standard' }, { id: 4, value: 'VVIP' }];
export var FinanceType = [{ id: 1, value: 'Credit' }, { id: 2, value: 'Debit' }];
export var FinanceItemType = [{ id: 1, value: 'Services' }, { id: 2, value: 'Products' }, { id: 3, value: 'Food' }, { id: 4, value: 'ExtraStay' }, { id: 5, value: 'Other' }];