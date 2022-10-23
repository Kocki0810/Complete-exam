<?php 

namespace App\Filters;

use Illuminate\Http\Request;

class ApiFilter {


    // If left empty all parameters will be allowed
    protected $safeParams = [
        // 'id' => ['gt']
    ];

    // Used to map columns that has a diffrent name in json
    // eg. if column name in db is 'id' and returned json is 'brugerID'
    // this is how map is done: 'brugerID' => 'id' 
    protected $columnMap = [];

    protected $operatorMap = [
        'eq' => '=',
        'gt' => '>',
        'gte' => '>=',
        'lt' => '<',
        'lte' => '<=',
        'ne' => '!=',
     ];

    public function transform(Request $request)
    {
        $eloQuery = [];
        if(count($this->safeParams) > 0)
        {
            foreach($this->safeParams as $param => $operators)
            {
                $query = $request->query($param);
                if(!isset($query))
                {
                    continue;
                }

                $column = $this->columnMap[$param] ?? $param;
                foreach($operators as $operator)
                {
                    if(isset($query[$operator]))
                    {
                        $eloQuery[] = [$column, $this->operatorMap[$operator], $query[$operator]];
                    }
                }
            }
        }
        else
        {
            $query = $request->query();
            foreach($query as $param => $operators)
            {
                $column = $this->columnMap[$param] ?? $param;
                foreach($operators as $operator => $value)
                {
                    if(!isset($this->operatorMap[$operator]))
                    {
                        abort(response("ILLEGAL OPERATION", 418));
                    }
                    if(isset($operators[$operator]))
                    {
                        $eloQuery[] = [$column, $this->operatorMap[$operator], $operators];
                    }
                }
            }
        }
        return $eloQuery;
    }
}

?>