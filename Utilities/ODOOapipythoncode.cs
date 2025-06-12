//from odoo.exceptions import UserError
//import json
//import base64
//import io
//import openpyxl
//from odoo import http
//from odoo.http import request, route
//from odoo import SUPERUSER_ID
//import requests
//from datetime import datetime
//#
//class WebsitePhysicalInventory(http.Controller) :

//    @http.route(['/process_stock_data'], type = 'json', auth = "public")
//    def physical_inventory_form(self,** post):
//        response = requests.Response()
//        if not post.get('stock'):
//            response.status_code = 404
//            response._content = b'{"Invalid Data, stock keyword missing": 65600}'

//            return { 'error' : 'stock keyword missing.'}
//response_stock = []
//        print(post.get('stock'))
//        company_domain = ['|', ('company_id', '=', False), ('company_id', '=', 1)]
//        for row in post.get('stock'):
//            try:
//                product_id = request.env['product.product'].sudo().search([('default_code', '=', row.get('ProductCode'))] + company_domain,limit = 1) 
//                location_id = request.env['stock.location'].sudo().search([('name', '=', row.get('WarehouseDescription'))] + company_domain,limit = 1)  
//                package_type_id = request.env['stock.package.type'].sudo().search([('name', '=', row.get('BoxDescription'))],limit = 1)
//                package_id = request.env['stock.quant.package'].sudo().search([('name', '=', row.get('BoxNumber'))],limit = 1) 
                
//                if not product_id:
//    product_id = request.env['product.product'].sudo().create({
//    'name' : row.get('ProductCode'),
//                                'default_code': row.get('ProductCode'),
//                            })
                        
//                if not location_id:
//    location_id = request.env['stock.location'].sudo().create({
//    'name': row.get('WarehouseDescription'),
//                                'replenish_location' : True,
//                            })
//                if not package_type_id:
//    package_type_id = request.env['stock.package.type'].sudo().create({
//    'name': row.get('BoxDescription'),
//                            })
                
//                if not package_id:
//    package_id = request.env['stock.quant.package'].sudo().create({
//    'name': row.get('BoxNumber'),
//                                    'shipping_weight': row.get('Weight'),
//                                    'location_id' : location_id.id,
//                                    'package_type_id' : package_type_id.id,
//                                })
//                stock_quant = request.env['stock.quant'].with_context(inventory_mode = True).with_user(2).create({
//    'location_id' : location_id.id,
//                                    'product_id' : product_id.id,
//                                    'package_id' : package_id.id,
//                                    'inventory_quantity': row.get('Quantity'),
//                                    'inventory_date' : datetime.now().date(),
                                    
//                                }).action_apply_inventory()
//                row.update({ 'status' : 'Successfull'})
//                response_stock.append(row)
//            except Exception as e:
//                row.update({ 'status' : 'Failed ' + str(e)})
//                response_stock.append(row)
//        return response_stock
//                # raise UserError(_("An error occurred while processing the file: %s" % str(e)))
//        # for record in post.get('stock'):



//    @http.route(['/stock_on_hand'], type = 'json', auth = "public")
//    def physical_inventory_stock_on_hand(self,** post):
//        return_list = []
//        company_domain = ['|', ('company_id', '=', False), ('company_id', '=', 1)]
//        domain = company_domain
//        if post.get('WarehouseDescription'):
//            domain = [('location_id.name', 'in', post.get('WarehouseDescription'))] + company_domain
//        grouped_quants = request.env['stock.quant'].sudo().read_group(
//domain = domain,
//        fields =['location_id', 'product_id', 'product_id.default_code', 'quantity:sum'],
//        groupby =['location_id', 'product_id'], lazy = False)
//        for record in grouped_quants:
//            item_code = ""
//            if record.get('product_id') and record.get('product_id')[1]:
//                item_code = record.get('product_id')[1].split('[')
//                if item_code and len(item_code) > 1:
//                    item_code = item_code[1]
//                    item_code = item_code.split(']')
//                    item_code = item_code[0]
//            return_list.append({
//    'warehouse' : record.get('location_id') and record.get('location_id')[1],
//                'Item Code' : item_code,
//                'Description' : record.get('product_id') and record.get('product_id')[1],
//                'Quantity' : record.get('quantity') and record.get('quantity'),
//                })
//        return return_list

//    @http.route(['/customer_outstanding_order'], type = 'json', auth = "public")
//    def physical_customer_outstanding_order(self,** post):
//        print("post============", post)
//        return_list = []
//        company_domain = ['|', ('company_id', '=', False), ('company_id', '=', 1)]
//        domain = [("order_id.state", '=', 'sale')] + company_domain
//        grouped_quants = request.env['sale.order.line'].sudo().read_group(
//domain = domain,
//        fields =['id', 'product_id', 'order_id', 'product_uom_qty', 'qty_delivered'],
//        groupby =['id'], lazy = False)

//        for record in grouped_quants:
//            print("record============", record)
//            print("record.get()", record.get('id')[0])
//            if record.get('id') and record.get('id')[0]:
//                sale_order_line = request.env['sale.order.line'].sudo().browse(record.get('id')[0])
//                today = datetime.today()

//                # Calculate the number of days difference
//                days_diff = (today - sale_order_line.order_id.date_order).days

//                return_list.append({

//    'Customer':sale_order_line.order_id.partner_id.name,
//                    'Order Number':sale_order_line.order_id.name,
//                    'Item Code' : sale_order_line.product_id.default_code,
//                    'Outstanding Quantity':sale_order_line.product_uom_qty - sale_order_line.qty_delivered,
//                    # 'Due Date':sale_order_line.order_id.due_date,
//                    'Order Date':sale_order_line.order_id.date_order,
//                    'Due Date':sale_order_line.order_id.commitment_date,
//                    '1-30':sale_order_line.product_uom_qty - sale_order_line.qty_delivered if days_diff <= 30 else 0.0,
//                    '31-60':sale_order_line.product_uom_qty - sale_order_line.qty_delivered if 31 <= days_diff <= 60  else 0.0, 
//                    '61-90':sale_order_line.product_uom_qty - sale_order_line.qty_delivered if 61 <= days_diff <= 90 else 0.0,
//                    '90+days':sale_order_line.product_uom_qty - sale_order_line.qty_delivered if days_diff >= 90 else 0.0,
//                    # 'Quantity' : record.get('quantity') and record.get('quantity'),
//                    })
        
//        return return_list