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
                    iconCls: 'icon-trigger',
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
                            iconCls: 'icon-add-trigger'
                        }, {
                            text: 'Delete',
                            id: 'DeleteTrigger',
                            disabled: true,
                            scope: this,
                            iconCls: 'icon-delete-trigger'
                        }
                        ]
                    }]
                });
                this.callParent(arguments);
            }
        });
        Ext.define('Pricing.ProductPortlet', {
            extend: 'Ext.grid.Panel',
            initComponent: function () {
                Ext.apply(this, {
                    height: this.height,
                    id: 'products-grid',
                    flex: 1,
                    title: 'Products',
                    iconCls: 'icon-trigger',
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
                    }],
                    dockedItems: [{
                        xtype: 'toolbar',
                        items: [{
                            text: 'Add',
                            scope: this,
                            iconCls: 'icon-add-trigger'
                        }, {
                            text: 'Delete',
                            id: 'DeleteTrigger',
                            disabled: true,
                            scope: this,
                            iconCls: 'icon-delete-trigger'
                        }
                        ]
                    }]
                });
                this.callParent(arguments);
            }
        });
        Ext.define('Pricing.PricelistPortlet', {
            extend: 'Ext.grid.Panel',
            CreatePricelist: function () {
                var popup = new Ext.Window({
                    width: 308,
                    height: 240,
                    title: 'Create Pricelist',
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
                                    boxLabel: "Public",
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
                    iconCls: 'icon-trigger',
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
                            iconCls: 'icon-add-trigger',
                            handler: this.CreatePricelist
                        }, {
                            text: 'Delete',
                            scope: this,
                            iconCls: 'icon-delete-trigger'
                        }, '-',
                        {
                            text: 'Clear Selection',
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
                        defaults: {
                            border: false
                        },
                        items: [{
                            region: 'west',
                            width: "20%",
                            minWidth: 150,
                            maxWidth: 400,
                            split: true,
                            collapsible: false,
                            layout: {
                                type: 'vbox',
                                align: 'stretch',
                                pack: 'start'
                            },
                            items: Ext.create('Pricing.ProductLinePortlet')
                        }, {
                            region: 'center',
                            layout: {
                                type: 'fit'
                            },
                            items: [
                                {
                                    layout: {
                                        type: 'vbox',
                                        align: 'stretch',
                                        pack: 'start'
                                    },
                                    items: [
                                        Ext.create('Pricing.ProductPortlet')
                                    ]
                                }]
                        },
                        {
                            region: 'east',
                            width: "55%",
                            layout: 'fit',
                            minWidth: 250,
                            split: true,
                            resizable: true,
                            items: [{
                                layout: {
                                    type: 'vbox',
                                    align: 'stretch',
                                    pack: 'start'
                                },
                                defaults: {
                                    border: false
                                },
                                items: [
                                    Ext.create('Pricing.PricelistPortlet')
                                   // Ext.create('Cloud.ErrorLogPortlet')
                                ]
                            }]
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

