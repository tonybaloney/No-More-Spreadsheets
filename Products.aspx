<%@ Page Title="Product Management View" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Pricelists" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Products" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Packages" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=ProductLines" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Users" ></script>
    <script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=PackageComponents" ></script>
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
            CreateProductLine: function (s,e,existing) {
               var popup = new Ext.Window({
                    width: 308,
                    height: 260,
                    title: (existing?'Edit Product Line':'Create Product Line'),
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
                        items: [
                            { 
                                xtype: 'hidden',
                                name: 'Id',
                                value: (existing?existing.data.Id:false)
                            },
                            {
                                xtype: 'textfield',
                                name: 'Name',
                                fieldLabel: 'Name',
                                allowBlank: false,
                                value: (existing?existing.data.Name:'')
                            },
                            {
                                xtype: 'textfield',
                                name: 'Description',
                                fieldLabel: 'Description',
                                allowBlank: false,
                                value: (existing?existing.data.Description:'')
                            },
                            {
                                xtype: 'textfield',
                                name: 'ProductManager',
                                fieldLabel: 'Product Manager',
                                allowBlank: true,
                                value: (existing?existing.data.ProductManager:'')
                            }
                        ],
                        buttons: [
                            {
                                text: (existing?'Save':'Create'),
                                formBind: true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: (existing?'QuoteService.svc/SaveProductLine':'QuoteService.svc/CreateProductLine'),
                                        jsonData: this.up('form').getForm().getValues(),
                                        success: function () {
                                            Ext.getStore('ProductLinesStore').load();
                                            popup.destroy();
                                        },
                                        failure: function (msg) { alert('Error creating/editing product line - '+msg); }
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
                    columnLines: true,
                    columns: [
                    {
                        text: 'Name',
                        flex: 1,
                        sortable: true,
                        dataIndex: 'Name',
                        xtype: 'templatecolumn',
                        tpl: '<h2>{Name}</h2> {Description}'
                    }, {
                        text: 'Product Manager',
                        width: 100,
                        dataIndex: 'ProductManager'
                    }
                    ],
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
                        },{
                            text: 'Edit',
                            scope: this,
                            icon: 'res/icons/package_green.png',
                            handler: function () { this.CreateProductLine(this,null,(this.selModel.selected.length > 0 ? this.selModel.selected.items[0] : null)); }
                        },{ xtype: 'tbfill' },
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
            CreateProduct: function (e,b,existing) {
                var popup = new Ext.Window({
                    width: 420,
                    height: 540,
                    constrain: true,
                    title: (existing?'Edit Product':'Create Product'),
                    icon: (existing?'res/icons/brick_edit.png':'res/icons/brick_add.png'),
                    items: {
                        xtype: 'form',
                        layout: { type: 'vbox', align: 'stretch' },
                        border: false,
                        bodyPadding: 10,
                        fieldDefaults: { labelWidth: 100 },
                        items: [
                                {
                                    xtype: "hidden",
                                    name: "Id",
                                    value: (existing?existing.data.Id:0)
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Title",
                                    name: 'Title',
                                    value: (existing?existing.data.Title:'')
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Group",
                                    name: 'Group',
                                    value: (existing?existing.data.Group:'')
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "SubGroup",
                                    name: 'SubGroup',
                                    value: (existing?existing.data.SubGroup:'')
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Partcode",
                                    name: 'Partcode',
                                    value: (existing?existing.data.Partcode:'')
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Manufacturer",
                                    name: 'Manufacturer',
                                    value: (existing?existing.data.Manufacturer:'')
                                },
                                {
                                    xtype: "textareafield",
                                    fieldLabel: "Description",
                                    name: 'Description',
                                    value: (existing?existing.data.Description:'')
                                },
                                {
                                    xtype: "textareafield",
                                    fieldLabel: "Internal Notes",
                                    name: 'InternalNotes',
                                    value: (existing?existing.data.InternalNotes:'')
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Availability",
                                    name: 'Availability',
                                    store: ['available','out of stock','on hold','end of sale','end of life'],
                                    selectOnFocus: true,
                                    allowBlank: false,
                                    value: (existing?existing.data.Availability:'')
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
                                     multiSelect: true,
                                     value: (existing?existing.data.ProductLines:'')
                                 }
                        ],
                        buttons: [
                            {
                                text: (existing?'Save':'Create'),
                                formBind: true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: (existing?'QuoteService.svc/SaveProduct':'QuoteService.svc/CreateProduct'),
                                        jsonData: this.up('form').getForm().getValues(),
                                        success: function () {
                                            Ext.getStore('ProductsStore').load();
                                            popup.destroy();
                                        },
                                        failure: function () { alert('Error creating/saving product.'); }
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
                    id: 'products-grid',
                    flex: 1,
                    title: 'Products',
                    icon: 'res/icons/brick.png',
                    store: 'ProductsStore',
                    border: false,
                    columnLines: true,
                    columns: [
                    {
                        text: 'Product',
                        flex: 3,
                        sortable: true,
                        dataIndex: 'Title',
                        xtype: 'templatecolumn', 
                        tpl: '<h2>{Title}</h2> {Description}'
                    },
                    {
                        text: 'Availability',
                        width: 100,
                        dataIndex: 'Availability'
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [{
                            text: 'Add Product',
                            scope: this,
                            icon: 'res/icons/brick_add.png',
                            handler:  this.CreateProduct
                        },{
                            text: 'Edit',
                            scope: this,
                            icon: 'res/icons/brick_edit.png',
                            handler: function () { this.CreateProduct(this, null, (this.selModel.selected.length > 0 ? this.selModel.selected.items[0] : null)); }
                        }, {
                            text: 'Delete',
                            scope: this,
                            icon: 'res/icons/brick_delete.png',
                            handler: this.DeleteProduct
                        },
                        /*{
                            xtype: 'combobox',
                            store: 'ProductLinesStore',
                            valueField: 'Id',
                            displayField: 'Name',
                            emptyText: 'Product Line..'
                            // TODO action filter on selection
                        }
                        ,*/{ xtype: 'tbfill' },
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
        Ext.define('Pricing.PackagePortlet', {
            extend: 'Ext.grid.Panel',
            DeletePackage: function () {
                if (Ext.getCmp('packages-grid').getSelectionModel().selected.length > 0) {
                    Ext.Msg.confirm('Delete?', 'Are you sure you want to delete?', function (button) {
                        if (button === 'yes') {
                            // do something when Yes was clicked.
                            Ext.Ajax.request({
                                url: 'QuoteService.svc/DeletePackage',
                                jsonData: { Id: Ext.getCmp('packages-grid').getSelectionModel().selected.items[0].data.Id },
                                success: function () { Ext.getStore('PackagesStore').load(); },
                                failure: function () { alert('Error deleting package.'); }
                            });
                        }
                    });
                }
            },
            
            CreatePackage: function (e, b, existing) {
                var popup = new Ext.Window({
                    width: 420,
                    height: 540,
                    constrain: true,
                    maximizable: true,
                    title: (existing ? 'Edit Package' : 'Create Package'),
                    icon: (existing ? 'res/icons/briefcase.png' : 'res/icons/briefcase.png'),
                    items:
                        {
                            xtype: 'tabpanel',
                            layout: { type: 'vbox', align: 'stretch' },
                            items: [
                            {
                                xtype: 'form',
                                title: 'Options',
                                layout: { type: 'vbox', align: 'stretch' },
                                border: false,
                                bodyPadding: 10,
                                fieldDefaults: { labelWidth: 100 },
                                items: [
                                        {
                                            xtype: "hidden",
                                            name: "Id",
                                            value: (existing ? existing.data.Id : 0)
                                        },
                                        {
                                            xtype: "textfield",
                                            fieldLabel: "Title",
                                            name: 'Title',
                                            value: (existing ? existing.data.Title : '')
                                        },
                                        {
                                            xtype: "checkbox",
                                            uncheckedValue: "false",
                                            fieldLabel: "Configurable",
                                            name: 'Configurable',
                                            value: (existing ? existing.data.Configurable : '')
                                        },
                                        {
                                            xtype: 'checkbox',
                                            uncheckedValue: "false",
                                            fieldLabel: 'Inherit pricing',
                                            name: 'InheritPrice',
                                            value: (existing ? existing.data.InheritPrice : '')
                                        },
                                        {
                                            xtype: 'checkbox',
                                            uncheckedValue: "false",
                                            fieldLabel: 'Inherit costing',
                                            name: 'InheritCost',
                                            value: (existing ? existing.data.InheritCost : '')
                                        },
                                        {
                                            xtype: "textfield",
                                            fieldLabel: "Manufacturer",
                                            name: 'Manufacturer',
                                            value: (existing ? existing.data.Manufacturer : '')
                                        },
                                        {
                                            xtype: "textfield",
                                            fieldLabel: "Partcode",
                                            name: 'Partcode',
                                            value: (existing ? existing.data.Partcode : '')
                                        },
                                        {
                                            xtype: "textareafield",
                                            fieldLabel: "Description Template",
                                            name: 'DescriptionTemplate',
                                            value: (existing ? existing.data.DescriptionTemplate : '')
                                        },
                                        {
                                            xtype: "combo",
                                            fieldLabel: "Availability",
                                            name: 'Availability',
                                            store: ['available', 'out of stock', 'on hold', 'end of sale', 'end of life'],
                                            selectOnFocus: true,
                                            allowBlank: false,
                                            value: (existing ? existing.data.Availability : '')
                                        },
                                         {
                                             xtype: "multiselect",
                                             fieldLabel: "Product Lines",
                                             store: 'ProductLinesStore',
                                             minSelections: 1,
                                             height: 150,
                                             valueField: 'Id',
                                             displayField: 'Name',
                                             name: 'ProductLines',
                                             multiSelect: true,
                                             value: (existing ? existing.data.ProductLines : '')
                                         }
                                ],
                                buttons: [
                                    {
                                        text: (existing ? 'Save' : 'Create'),
                                        formBind: true,
                                        handler: function () {
                                            Ext.Ajax.request({
                                                url: (existing ? 'QuoteService.svc/SavePackage' : 'QuoteService.svc/CreatePackage'),
                                                jsonData: this.up('form').getForm().getValues(),
                                                success: function (a,b,c) {
                                                    // Attach components to the package.
                                                    if (existing) {
                                                        packageId = existing.data.Id;

                                                        // Get the package components from the store.
                                                        var componentStore = popup.down('gridpanel').store;
                                                        componentStore.data.items
                                                        var data_array = Array();
                                                        for (var i = 0; i < componentStore.data.length; i++) {
                                                            data_array.push(componentStore.data.items[i].data);
                                                        }
                                                        //var data_string = Ext.JSON.encode(data_array);
                                                        Ext.Ajax.request({
                                                            url: 'QuoteService.svc/SavePackageComponents',
                                                            jsonData: { OwningPackageId:packageId, Components: data_array },
                                                            success: function () {
                                                                Ext.getStore('PackagesStore').load();
                                                                popup.destroy();
                                                            },
                                                            failure: function () {
                                                                alert("Could not add package components to saved package");
                                                            }
                                                        });
                                                    }
                                                },
                                                failure: function () { alert('Error creating/saving package.'); }
                                            });
                                        }
                                    }
                                    //,{ text:'?',handler : function () { alert ( this.ownerCt.ownerCt.ownerCt.width + " x " + this.ownerCt.ownerCt.ownerCt.height ) ; } } 
                                ]
                            }, {
                                xtype: 'gridpanel',
                                title: 'Components',
                                store: Ext.create('Ext.data.Store', {
                                    autoLoad: true,
                                    proxy : { type: 'ajax', url: 'Data.aspx?model=PackageComponents&view=Data&PackageId='+existing.data.Id, reader: { type: 'json' } },
                                    fields : [
                                            { name: "Title", type: "string" },
                                            { name: "AllowMultiple", type: "bool" },
                                            { name: "ProductsString", type: "string" }
                                            ]
                                }),
                                disabled: (existing ? false : true),
                                dockedItems: [{
                                    xtype: 'toolbar',
                                    items: [{
                                        text: 'Add Component',
                                        icon: 'res/icons/add.png',
                                        handler: function (sender, event) {
                                            var popup = new Ext.Window({
                                                width: 420,
                                                height: 320,
                                                constrain: true,
                                                title: 'Add Component',
                                                icon: 'res/icons/briefcase.png',
                                                items:
                                                    {
                                                        xtype: 'form',
                                                        title: 'Component Options',
                                                        layout: { type: 'vbox', align: 'stretch' },
                                                        border: false,
                                                        bodyPadding: 10,
                                                        fieldDefaults: { labelWidth: 100 },
                                                        items: [
                                                                {
                                                                    xtype: "checkbox",
                                                                    name: "AllowMultiple",
                                                                    uncheckedValue: "false",
                                                                    fieldLabel: "Allow Multiple?"
                                                                },
                                                                {
                                                                    xtype: "textfield",
                                                                    fieldLabel: "Title",
                                                                    name: 'Title'
                                                                },
                                                                {
                                                                    xtype: "multiselect",
                                                                    fieldLabel: "Products",
                                                                    store: 'ProductsStore',
                                                                    minSelections: 1,
                                                                    height: 150,
                                                                    valueField: 'Id',
                                                                    displayField: 'Title',
                                                                    name: 'ProductsString',
                                                                    multiSelect: true
                                                                }
                                                        ],
                                                        buttons: [
                                                            {
                                                                text: 'Create',
                                                                formBind: true,
                                                                handler: function () {
                                                                    var newRec = this.ownerCt.ownerCt.form.getValues();
                                                                    sender.ownerCt.ownerCt.store.add(newRec);
                                                                    popup.destroy();
                                                                }
                                                            }
                                                        ]
                                                    }
                                            });
                                            popup.show(this);
                                            popup.center();
                                        }
                                    }, {
                                        text: 'Remove Component',
                                        icon: 'res/icons/delete.png',
                                        handler: function () {
                                            var ax = this.ownerCt.ownerCt;
                                            if (ax.selModel.selected.length > 0)
                                            ax.store.remove(ax.selModel.selected.items[0]); 
                                        }
                                    }]
                                }],
                                columns: [
                                    { xtype: 'rownumberer' },
                                    { header: 'Title', dataIndex: 'Title' },
                                    { header: 'Allow Multiple', dataIndex: 'AllowMultiple' }
                                ]
                            }
                            ]
                        }
                });
                popup.show(this);
                popup.center();
            },
            initComponent: function () {
                Ext.apply(this, {
                    height: this.height,
                    id: 'packages-grid',
                    flex: 1,
                    title: 'Packages',
                    icon: 'res/icons/briefcase.png',
                    store: 'PackagesStore',
                    border: false,
                    columnLines: true,
                    columns: [
                    {
                        text: 'Package',
                        flex: 3,
                        sortable: true,
                        dataIndex: 'Title',
                        xtype: 'templatecolumn',
                        tpl: '<h2>{Title}</h2> {Description}'
                    },
                    {
                        text: 'Availability',
                        width: 100,
                        dataIndex: 'Availability'
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [ {
                            text: 'Add Package',
                            scope: this,
                            icon: 'res/icons/briefcase.png',
                            handler: this.CreatePackage
                        }, {
                            text: 'Edit',
                            scope: this,
                            icon: 'res/icons/brick_edit.png',
                            handler: function () { this.CreatePackage(this, null, (this.selModel.selected.length > 0 ? this.selModel.selected.items[0] : null)); }
                        }, {
                            text: 'Delete',
                            scope: this,
                            icon: 'res/icons/brick_delete.png',
                            handler: this.DeletePackage
                        },
                        /*{
                            xtype: 'combobox',
                            store: 'ProductLinesStore',
                            valueField: 'Id',
                            displayField: 'Name',
                            emptyText: 'Product Line..'
                            // TODO action filter on selection
                        }
                        ,*/ { xtype: 'tbfill' },
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
            CreatePricelist: function (e,b,existing) {
                var popup = new Ext.Window({
                    width: 420,
                    height: 235,
                    title: (existing?'Edit Pricelist':'Create Pricelist'),
                    icon: (existing?'res/icons/table_add.png':'res/icons/table_add.png'),
                    items: {
                        xtype: 'form',
                        layout: { type: 'vbox', align: 'stretch' },
                        border: false,
                        bodyPadding: 10,
                        fieldDefaults: { labelWidth: 100 },
                        items: [
                                { 
                                    xtype: 'hidden',
                                    name: 'Id',
                                    value: (existing?existing.data.Id:0)
                                },
                                {
                                    xtype: "textfield",
                                    fieldLabel: "Name",
                                    name: 'Name',
                                    value: (existing?existing.data.Name:"")
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Owner",
                                    store: 'UsersStore',
                                    valueField: 'Id',
                                    displayField: 'RealName',
                                    hiddenName: 'pricelistowner',
                                    editable: false,
                                    name: 'OwnerId',
                                    value: (existing?existing.data.OwnerId:'')
                                }, {
                                    xtype: "multiselect",
                                    fieldLabel: "Product Lines",
                                    store: 'ProductLinesStore',
                                    valueField: 'Id',
                                    displayField: 'Name',
                                    name: 'ProductLines',
                                    multiSelect: true,
                                    value: (existing?existing.data.ProductLines:'')
                                },
                                {
                                    xtype: "checkbox",
                                    fieldLabel: "Public Pricelist",
                                    name: 'IsPublic',
                                    value: (existing?existing.data.IsPublic:false)
                                },
                                {
                                    xtype: "combo",
                                    fieldLabel: "Currency",
                                    name: 'Currency',
                                    store: ['GBP', 'AUD', 'USD', 'EUR'],
                                    selectOnFocus: true,
                                    allowBlank: false,
                                    value: (existing ? existing.data.Currency : 'USD')
                                }
                            ],
                        buttons: [
                            {
                                text: 'Create',
                                formBind:true,
                                handler: function () {
                                    Ext.Ajax.request({
                                        url: (existing?'QuoteService.svc/SavePricelist':'QuoteService.svc/CreatePricelist'),
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
                        },
                        {
                            text: 'Edit',
                            scope: this,
                            icon: 'res/icons/table_edit.png',
                            handler: function () { this.CreatePricelist(this,null,(this.selModel.selected.length > 0 ? this.selModel.selected.items[0] : null)); }
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
                            layout: {
                                type: 'vbox',
                                align: 'stretch',
                                pack: 'start'
                            },
                            items: [Ext.create('Pricing.ProductPortlet'),Ext.create('Pricing.PackagePortlet')]
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

