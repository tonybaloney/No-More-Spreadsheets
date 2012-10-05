<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Pricelists" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Products" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=ProductLines" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Users" ></script>
    <script type="text/javascript">
        Ext.Loader.setConfig({ enabled: true });
        Ext.Loader.setPath('Ext.ux', 'ext/examples/ux');
        Ext.require([
            'Ext.form.Panel',
            'Ext.ux.form.MultiSelect',
            'Ext.ux.form.ItemSelector'
        ]);
        Ext.define('Pricing.ProductLinePortlet', {
            extend: 'Ext.grid.Panel',
            DeleteProductLine: function () {
                if (Ext.getCmp('product-line-grid').getSelectionModel().selected.length > 0) {
                    Ext.Msg.confirm('Delete?', 'Are you sure you want to delete?', function (button) {
                        if (button === 'yes') {
                            // do something when Yes was clicked.
                            Ext.Ajax.request({
                                url: 'QuoteService.svc/DeleteProductLine',
                                jsonData: { Id: Ext.getCmp('product-line-grid').getSelectionModel().selected.items[0].data.Id },
                                success: function () { Ext.getStore('ProductLinesStore').load(); },
                                failure: function () { alert('Error deleting pricelist'); }
                            });
                        }
                    });
                }
            },
            CreateProductLine: function () {
               var popup = new Ext.Window({
                    width: 308,
                    height: 240,
                    title: 'Create Product Line',
                    layout: 'fit',
                    plain: true,
                    items: {
                        xtype: 'form',
                        layout: {
                            type: 'vbox',
                            align: 'stretch'
                        },
                        border: false,
                        bodyPadding: 10,
                        fieldDefaults: {
                            labelAlign: 'top',
                            labelWidth: 100,
                            labelStyle: 'font-weight:bold'
                        },
                        defaults: {
                            margins: '0 0 10 0'
                        },
                        url: 'QuoteService.svc/CreateProductLine',
                        items: [
                            {
                                xtype: 'textfield',
                                name: 'Name',
                                fieldLabel: 'Name',
                                allowBlank: false
                            },
                            {
                                xtype: 'textfield',
                                name: 'Description',
                                fieldLabel: 'Description',
                                allowBlank: false
                            },
                            {
                                xtype: 'textfield',
                                name: 'ProductManager',
                                fieldLabel: 'Product Manager',
                                allowBlank: true
                            }
                        ],
                        buttons: [
                            {
                                text: 'Create',
                                formBind: true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: 'QuoteService.svc/CreateProductLine',
                                        jsonData: this.up('form').getForm().getValues(),
                                        success: function () {
                                            Ext.getStore('ProductLinesStore').load();
                                            popup.destroy();
                                        },
                                        failure: function () { alert('Error closing quote'); }
                                    });
                                }
                            }
                        ]
                    }                     
                });
                popup.show(this);
                popup.center();
            },
            initComponent: function () {
                Ext.apply(this, {
                    id: 'product-line-grid',
                    flex: 1,
                    title: 'Product Lines',
                    icon: 'res/icons/package.png',
                    store: 'ProductLinesStore',
                    border: false,
                    stripeRows: true,
                    columnLines: true,
                    columns: [
                    {
                        text: 'Name',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'Name'
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [{
                            text: 'Add',
                            scope: this,
                            handler: this.CreateProductLine,
                            icon: 'res/icons/package_add.png'
                        }, {
                            text: 'Delete',
                            scope: this,
                            icon: 'res/icons/package_delete.png',
                            handler: this.DeleteProductLine
                        }, { xtype: 'tbfill' },
                        {
                            text: 'Clear Selection',
                            icon: 'res/icons/bullet_white.png',
                            handler: function () {
                                this.ownerCt.ownerCt.getSelectionModel().deselectAll();
                            }
                        }
                        ]
                    }]
                });
                this.callParent(arguments);
            }
        });
        Ext.define('Pricing.ProductPortlet', {
            extend: 'Ext.grid.Panel',
            DeleteProduct: function () {
                if (Ext.getCmp('products-grid').getSelectionModel().selected.length > 0) {
                    Ext.Msg.confirm('Delete?', 'Are you sure you want to delete?', function (button) {
                        if (button === 'yes') {
                            // do something when Yes was clicked.
                            Ext.Ajax.request({
                                url: 'QuoteService.svc/DeleteProduct',
                                jsonData: { Id: Ext.getCmp('products-grid').getSelectionModel().selected.items[0].data.Id },
                                success: function () { Ext.getStore('ProductsStore').load(); },
                                failure: function () { alert('Error deleting product.'); }
                            });
                        }
                    });
                }
            },
            CreateProduct: function () {
                var popup = new Ext.Window({
                    width: 420,
                    height: 540,
                    title: 'Create Product',
                    icon: 'res/icons/brick_add.png',
                    items: {
                        xtype: 'form',
                        layout: { type: 'vbox', align: 'stretch' },
                        border: false,
                        bodyPadding: 10,
                        fieldDefaults: { labelWidth: 100 },
                        items: [
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Title",
                                    name: 'Title'
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Group",
                                    name: 'Group'
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Subgroup",
                                    name: 'Subgroup'
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Partcode",
                                    name: 'Partcode'
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Manufacturer",
                                    name: 'Manufacturer'
                                },
                                {
                                    xtype: "textareafield",
                                    fieldLabel: "Description",
                                    name: 'Description'
                                },
                                {
                                    xtype: "textareafield",
                                    fieldLabel: "Internal Notes",
                                    name: 'InternalNotes'
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Availability",
                                    name: 'Availability',
                                    store: ['available','out of stock','on hold','end of sale','end of life'],
                                    selectOnFocus: true,
                                    allowBlank: false
                                },
                                 {
                                     xtype: "multiselect",
                                     fieldLabel: "Product Lines",
                                     store: 'ProductLinesStore',
                                     minSelections:1,
                                     height: 150,
                                     valueField: 'Id',
                                     displayField: 'Name',
                                     name: 'ProductLines',
                                     multiSelect: true
                                 }
                        ],
                        buttons: [
                            {
                                text: 'Create',
                                formBind: true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: 'QuoteService.svc/CreateProduct',
                                        jsonData: this.up('form').getForm().getValues(),
                                        success: function () {
                                            Ext.getStore('ProductsStore').load();
                                            popup.destroy();
                                        },
                                        failure: function () { alert('Error creating product.'); }
                                    });
                                }
                            }
                            ,{ text:'?',handler : function () { alert ( this.ownerCt.ownerCt.ownerCt.width + " x " + this.ownerCt.ownerCt.ownerCt.height ) ; } } 
                        ]
                    }
                });
                popup.show(this);
                popup.center();
            },
            initComponent: function () {
                Ext.apply(this, {
                    height: this.height,
                    id: 'products-grid',
                    flex: 1,
                    title: 'Products',
                    icon: 'res/icons/brick.png',
                    store: 'ProductsStore',
                    border: false,
                    stripeRows: true,
                    columnLines: true,
                    columns: [
                    {
                        text: 'Title',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'Title'
                        // TODO create template to show all product attributes on row item
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [{
                            text: 'Add',
                            scope: this,
                            icon: 'res/icons/brick_add.png',
                            handler:  this.CreateProduct
                        }, {
                            text: 'Delete',
                            scope: this,
                            icon: 'res/icons/brick_delete.png',
                            handler: this.DeleteProduct
                        },
                        {
                            xtype: 'combobox',
                            store: 'ProductLinesStore',
                            valueField: 'Id',
                            displayField: 'Name',
                            emptyText: 'Product Line..'
                            // TODO action filter on selection
                        }
                        ,{ xtype: 'tbfill' },
                        {
                            text: 'Clear Selection',
                            icon: 'res/icons/bullet_white.png',
                            handler: function () {
                                this.ownerCt.ownerCt.getSelectionModel().deselectAll();
                            }
                        }
                        ]
                    }]
                });
                this.callParent(arguments);
            }
        });
        Ext.define('Pricing.PricelistPortlet', {
            extend: 'Ext.grid.Panel',
            DeletePricelist: function () {
                if (Ext.getCmp('pricelists-grid').getSelectionModel().selected.length > 0) {
                    Ext.Msg.confirm('Delete?', 'Are you sure you want to delete?', function (button) {
                        if (button === 'yes') {
                            // do something when Yes was clicked.
                            Ext.Ajax.request({
                                url: 'QuoteService.svc/DeletePricelist',
                                jsonData: { Id: Ext.getCmp('pricelists-grid').getSelectionModel().selected.items[0].data.Id},
                                success: function () { Ext.getStore('PricelistsStore').load();  },
                                failure: function () { alert('Error deleting pricelist'); }
                            });
                        }
                    });
                }
            },
            CreatePricelist: function () {
                var popup = new Ext.Window({
                    width: 420,
                    height: 235,
                    title: 'Create Pricelist',
                    icon: 'res/icons/table_add.png',
                    items: {
                        xtype: 'form',
                        layout: { type: 'vbox', align: 'stretch' },
                        border: false,
                        bodyPadding: 10,
                        fieldDefaults: { labelWidth: 100 },
                        items: [
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Title",
                                    name: 'Title'
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Owner",
                                    store: 'UsersStore',
                                    valueField: 'Id',
                                    displayField: 'RealName',
                                    hiddenName: 'pricelistowner',
                                    editable: false,
                                    name: 'OwnerId'
                                }, {
                                    xtype: "multiselect",
                                    fieldLabel: "Product Lines",
                                    store: 'ProductLinesStore',
                                    valueField: 'Id',
                                    displayField: 'Name',
                                    name: 'ProductLines',
                                    multiSelect: true
                                },
                                {
                                    xtype: "checkbox",
                                    fieldLabel: "Public Pricelist",
                                    name: 'IsPublic'
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Currency",
                                    name: 'Currency',
                                    store: ['GBP', 'AUD', 'USD', 'EUR'],
                                    selectOnFocus: true,
                                    allowBlank: false
                                }
                            ],
                        buttons: [
                            {
                                text: 'Create',
                                formBind:true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: 'QuoteService.svc/CreatePricelist',
                                        jsonData: this.up('form').getForm().getValues(),
                                        success: function () {
                                            Ext.getStore('PricelistsStore').load();
                                            popup.destroy();
                                        },
                                        failure: function () { alert('Error creating pricelist'); }
                                    });
                                }
                            }
                            //,{ text:'?',handler : function () { alert ( this.ownerCt.ownerCt.ownerCt.width + " x " + this.ownerCt.ownerCt.ownerCt.height ) ; } } 
                        ]
                    }
                });
                popup.show(this);
                popup.center();
            },
            initComponent: function () {
                Ext.apply(this, {
                    height: this.height,
                    id: 'pricelists-grid',
                    flex: 1,
                    title: 'Pricelists',
                    icon: 'res/icons/table.png',
                    store: 'PricelistsStore',
                    border: false,
                    stripeRows: true,
                    columnLines: true,
                    columns: [
                    {
                        text: 'Name',
                        flex: 3,
                        sortable: true,
                        dataIndex: 'Name'
                    },
                    {
                        text: 'Date',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'Date'
                    },
                    {
                        text: 'Currency',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'Currency'
                    },
                    {
                        text: 'Owner',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'OwnerName'
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [{
                            text: 'Add',
                            scope: this,
                            icon: 'res/icons/table_add.png',
                            handler: this.CreatePricelist
                        }, {
                            text: 'Delete',
                            scope: this,
                            icon: 'res/icons/table_delete.png',
                            handler: this.DeletePricelist
                        }, { xtype: 'tbfill' },
                        {
                            text: 'Clear Selection',
                            icon: 'res/icons/bullet_white.png',
                            handler: function () {
                                this.ownerCt.ownerCt.getSelectionModel().deselectAll();
                            }
                        }
                        ]
                    }]
                });
                this.callParent(arguments);
            }
        });
        Ext.define('Ext.app.Portal', {
            extend: 'Ext.container.Viewport',
            initComponent: function () {
                Ext.apply(this, {
                    layout: {
                        type: 'border',
                        padding: '0 0 0 0' // pad the layout from the window edges
                    },
                    items: [
                        {
                        xtype: 'container',
                        region: 'center',
                        layout: 'border',
                        items: [{
                            region: 'west',
                            flex: 1,
                            layout: {
                                type: 'vbox',
                                align: 'stretch',
                                pack: 'start'
                            },
                            items: Ext.create('Pricing.ProductLinePortlet')
                        }, {
                            region: 'center',
                            flex:1,
                            layout: 'fit',
                            items: Ext.create('Pricing.ProductPortlet')
                        },
                        {
                            region: 'east',
                            flex:1,
                            layout: 'fit',
                            items: Ext.create('Pricing.PricelistPortlet')
                        }]
                    }]
                });
                this.callParent(arguments);
            },

            showMsg: function (msg) {
                var el = Ext.get('app-msg'),
                    msgId = Ext.id();

                this.msgId = msgId;
                el.update(msg).show();

                Ext.defer(this.clearMsg, 3000, this, [msgId]);
            },

            clearMsg: function (msgId) {
                if (msgId === this.msgId) {
                    Ext.get('app-msg').hide();
                }
            }
        });
    </script>
    <script type="text/javascript">
        Ext.onReady(function(){
            Ext.create('Ext.app.Portal');
        });
    </script>
</asp:Content>

