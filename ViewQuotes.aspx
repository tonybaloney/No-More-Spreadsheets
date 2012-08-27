<%@ Page Title="My Quotes" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewQuotes.aspx.cs" Inherits="ViewQuotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="ext/icon_packs/fugue/css/fugue-pack.css" />
<script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Pricelists" ></script>
<script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Users" ></script>
<script type="text/javascript" src="Data.aspx?view=ExtModelAndStore&model=Quotes" ></script>
<script type="text/javascript" src="pricing/js/Renderers.js"></script>
<script type="text/javascript">
    Ext.onReady(function(){
        function WithDiscount (val, rec,c ) {
            var total = parseFloat(c.data.q_totalvalue) * (1-parseFloat(c.data.q_discount_percent)/100) - parseFloat(c.data.q_discount_writein) ;
            return ukMoney (total);
        }
        function CustomerName (val) {
            return '<span><a href="javascript:alert(\'to salesforce\')">'+val+'</a></span>';
        }
        function GetSelectedQuote(){
            return grid.selModel.selected.items[0].data.q_id;
        }
        function RenewQuote(){
            Ext.Msg.show({
                title:'Renew quote?',
                msg: 'This will refresh the items in the quote using the newest price list, are you sure?',
                buttons: Ext.Msg.OKCANCEL,
                fn: function(e){
                    if (e=='ok')
                        window.location = 'quote.php?mode=renew&quoteid=';
                },
                icon: Ext.MessageBox.QUESTION
            });
        }
        function CloseQuote(){
            Ext.Msg.show({
                title:'Close quote?',
                msg: 'Are you sure you wish to close the selected quote(s)?',
                buttons: Ext.Msg.OKCANCEL,
                fn: function(e){
                    if (e=='ok') {
                        Ext.Ajax.request({
                            url: 'quote.php?mode=close&quoteid='+GetSelectedQuote(),
                            success: function() {Ext.getStore('QuotesStore').load();} ,
                            failure: function() { alert('Error closing quote'); }
                        });
                    }
                },
                icon: Ext.MessageBox.QUESTION
            });
        }

        function DeleteQuote(){
            Ext.Msg.show({
                title:'Delete quote?',
                msg: 'Are you sure you wish to delete the selected quote(s)?',
                buttons: Ext.Msg.OKCANCEL,
                fn: function(e){
                    if (e=='ok') {
                        Ext.Ajax.request({
                            url: 'quote.php?mode=close&delete=yes&quoteid='+GetSelectedQuote(),
                            success: function() {Ext.getStore('QuotesStore').load();} ,
                            failure: function() { alert('Error deleting quote'); }
                        });
                    }
                },
                icon: Ext.MessageBox.QUESTION
            });
        }
        function MakeTemplate(){
            Ext.Msg.show({
                title:'Make template quote?',
                msg: 'Are you sure you wish to make a template from the selected quote(s)?',
                buttons: Ext.Msg.OKCANCEL,
                fn: function(e){
                    if (e=='ok') {
                        Ext.Ajax.request({
                            url: 'quote.php?mode=close&opt=template&quoteid='+GetSelectedQuote(),
                            success: function() {Ext.getStore('QuotesStore').load();} ,
                            failure: function() { alert('Error closing quote'); }
                        });
                    }
                },
                icon: Ext.MessageBox.QUESTION
            });
        }
        function AssignQuote(){
            if (!GetSelectedQuote()) return;
            var popup = new Ext.Window({
                layout:'fit',
                width:420,
                height:105,
                closeAction:'hide',
                plain: true,
                resizable:false,
                title: 'Assign Quote',
                items: {
                    xtype:'form',
                    frame: true,
                    hideBorders:true,
                    items: [
                        new Ext.form.ComboBox({
                            store: 'UsersStore',
                            valueField:'objectid',
                            displayField:'userrealname',
                            hiddenName:'q_owner_id',
                            typeAhead:true,
                            mode:'local',
                            editable:false,
                            fieldLabel: 'Quote Owner',
                            name: 'q_owner',
                            allowBlank: false
                        })
                    ],
                    buttons: [{ text:'Assign', handler: function(){
                        var new_id = this.ownerCt.ownerCt.form.items[0].value  ;
                        if (new_id){
                            Ext.Ajax.request({
                                url: 'pricing/data.php?type=assign&user='+new_id+'&quoteid='+GetSelectedQuote(),
                                success: function() {Ext.getStore('QuotesStore').load();} ,
                                failure: function() { alert('Error assigning quote'); }
                            });
                            this.ownerCt.ownerCt.ownerCt.hide();
                        }
                    }}
                    ]
                }
            }); 
            popup.show(this);
            popup.center();
        }

        var pricelistCombo = new Ext.form.ComboBox({	
            name:'q_pricelist',
            store: 'PricelistsStore',
            displayField:'pl_name',
            fieldLabel : 'Pricelist',
            valueField: 'pl_id',
            mode: 'local',
            editable : false,
            listConfig: {
                getInnerTpl :  function() { return '<div><h3><span>{pl_name}</span></h3>Currency: {pl_currency}<div>'; }
            },
            allowBlank: false
        });
        function CreateQuote( templateid ){
            Ext.create('Ext.window.Window', {
                layout:'fit',
                plain: true,
                width:340,
                height:250,
                closeAction:'hide',
                resizable:false,
                title: 'Create quote',
                iconCls:'icon-fugue-money-coin',
                items: [
                    new Ext.FormPanel({
                        url: 'CreateQuote.aspx',
                        standardSubmit: true, // move urls on submit, non ajax
                        layout: 'anchor',
                        border: false,
                        frame: true,
                        defaults: { anchor: '100%' },
                        defaultType: 'textfield',
                        bodyPadding: 5,
                        items: [
                            { 
                                name: 'quote_create',
                                xtype:'hidden',
                                value: 'yes'
                            },
                            {
                                fieldLabel: 'Title of quote',
                                name: 'q_title',
                                allowBlank: false
                            },
                            new Ext.form.ComboBox({
                                store: 'UsersStore',
                                valueField:'objectid',
                                displayField:'userrealname',
                                triggerAction:'all',
                                mode:'local',
                                editable:false,
                                fieldLabel: 'Quote Owner',
                                name: 'q_owner',
                                allowBlank: false
                            }),
                            { 
                                id: 'cloneid',
                                name: 'cloneid',
                                xtype:'hidden',
                                value: templateid
                            },
                            {
                                id: 'q_customer_name',
                                fieldLabel: 'Account',
                                name: 'q_customer_name',
                                value: '',
                                allowBlank: false,
                                disabled: false
                            }
                            ,pricelistCombo
                        ],
                        buttons: [{
                            text: 'Create Quote', 
                            formBind: true, // Only on once form is valid
                            disabled: false, // Off to start with.
                            handler : function () { this.ownerCt.ownerCt.form.submit({ clientValidation: true }) ; }
                        }]
                    })
                ]
            }).show();
        }
        // create the Grid
        var grid = new Ext.grid.GridPanel({
            forceFit: true,
            store: 'QuotesStore',
            columns: [
				{id:'q_id',header: 'ID', width: 50, sortable: true, dataIndex: 'q_id'},
				{header: 'Customer', width: 320, sortable: true, dataIndex: 'q_customer_name', renderer:CustomerName, flex:1},
				{id:'q_title',header: 'Title', width: 75, sortable: true,  dataIndex: 'q_title', flex:2},
				{header: 'Owner', width: 150, sortable: true, dataIndex: 'q_ownername' },
				{header: 'Value', width: 75, sortable: true, dataIndex: 'q_totalvalue', renderer:WithDiscount},
				{header: 'Pricelist', width:200, sortable: false, dataIndex: 'q_pricelist_name'},
				{header: 'Status', width: 75, sortable: true, dataIndex: 'q_status'},
				{header: 'Last Updated', width: 150, sortable: true, dataIndex: 'q_last_change'}
            ],
            listeners : { 
                itemdblclick :function( ){
                    window.location = 'quote.php?mode=edit&quoteid='+GetSelectedQuote();
                }
            },
            tbar: new Ext.Toolbar({
                enableOverflow: true,
                items: [
                        { xtype:'button', text:'Create Quote', handler: function () {CreateQuote();} , iconCls:'icon-fugue-money-coin'},
                        { xtype:'button', text:'Edit Quote', handler: function () { window.location = 'quote.php?mode=edit&quoteid='+GetSelectedQuote();} , iconCls:'icon-fugue-document-text'},
                        { xtype:'button', text:'Use as template', handler: function () { CreateQuote(GetSelectedQuote());} , iconCls:'icon-fugue-documents'},
                        //{ xtype:'button', text:'Renew Quote', handler: RenewQuote , iconCls:'icon-fugue-arrow-circle'},
                        { xtype:'button', text:'Make Template', handler: MakeTemplate , iconCls:'icon-fugue-script-block'},
                        { xtype: 'tbfill' },
                        { xtype:'button', text: 'Assign', handler : AssignQuote, iconCls:'icon-fugue-user'},
                        { xtype:'button', text:'Print PDF', handler: function () { window.location = 'quote.php?mode=print&format=pdf&quoteid='+GetSelectedQuote();} , iconCls:'icon-fugue-document-pdf'},
                        { xtype:'button', text:'Close Quote', handler: CloseQuote , iconCls:'icon-fugue-door'},
                        'Search:',
                        {xtype:'textfield', width:200, id:'search-field', listeners: { specialkey: function(f,e){ if (e.getKey() == e.ENTER) { Ext.data.StoreManager.lookup('QuotesStore').load( {params: {viewOpt:'search', searchString:Ext.getCmp('search-field').getValue()} }); } } } },
                        {xtype:'button', text:'Go', handler: function(){Ext.data.StoreManager.lookup('QuotesStore').load( {params: {viewOpt:'search', searchString:Ext.getCmp('search-field').getValue()} })}}
                ]})
        });
        var quotes = new Ext.Panel({
            layout:'fit',
            region: 'center',
            items:grid
        });
        var views = new Ext.tree.TreePanel({
            title: 'My Quotes',
            iconCls:'icon-fugue-user',
            root: {
                expanded: true,
                children: [
					{
					    text: 'Open Quotes',
					    viewOpt : '',
					    iconCls: 'icon-fugue-door-open',
					    leaf: true
					}, {
					    text: 'Closed Quotes',
					    viewOpt : 'closed',
					    iconCls: 'icon-fugue-door',
					    leaf: true
					}, {
					    text: 'This Month',
					    viewOpt : 'thismonth',
					    iconCls: 'icon-fugue-calendar-month',
					    leaf: true
					}, {
					    text: 'Last Month',
					    viewOpt : 'lastmonth',
					    iconCls: 'icon-fugue-calendar-previous',
					    leaf: true
					}
                ]
            },
            rootVisible: false,
            listeners: {
                itemclick: function(t,n) {
                    if ( n.raw.leaf ) 
                        Ext.data.StoreManager.lookup('QuotesStore').load( {params: {viewOpt:n.raw.viewOpt, viewOptExtra:n.raw.viewOptExtra} }) ;
                }
            }
        });
        
    var templatesView = new Ext.tree.TreePanel ({
        title: 'Templates',
        iconCls:'icon-fugue-script-block',
        root: {
            expanded: true,
            children: [{ text: 'Quote Templates',
                leaf :true,
                viewOpt:'templates',
                viewOptExtra:'',
                iconCls:'icon-fugue-script-block'}
            ]
        },
        rootVisible: false,
        listeners: {
            itemclick: function(t,n) {
                Ext.data.StoreManager.lookup('QuotesStore').load( {params: {viewOpt:n.raw.viewOpt, viewOptExtra:n.raw.viewOptExtra} }) ;
            }
        }
    });
    var accordion = Ext.create('Ext.Panel', {
        region:'west',
        margins:'5 0 5 5',
        split:true,
        width: 210,
        layout:'accordion',
        items: [views,templatesView]
    });
		
    new Ext.Viewport({
        layout: 'border',
        plain:true,
        items: [accordion,quotes],
        renderTo: document.body
    });
    });
</script>
</asp:Content>

